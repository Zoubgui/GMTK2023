using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private float speed =75f;
    [SerializeField] Rigidbody2D rb;
    private float maxVelocity =4f;
    
    public SpriteRenderer sprite;
    public Animator animator;

    AudioSource wallSoundEffect;
    AudioSource trapSoundEffect;
    AudioSource ennemiSoundEffect;

    public GameObject fxWall;

    public bool damageTaken = false;

    private Gyroscope gyro;
    private Quaternion initialRotation;



    void Start()
    {
       

        StartCoroutine(WaitidleState(GameManager.instance.idleWait));
     
        wallSoundEffect = transform.GetChild(1).GetComponent<AudioSource>();
        Debug.Log(wallSoundEffect);
        trapSoundEffect = transform.GetChild(2).GetComponent<AudioSource>();
        ennemiSoundEffect = transform.GetChild(3).GetComponent<AudioSource>();


        // V�rifier si le gyroscope est disponible sur l'appareil
        if (SystemInfo.supportsGyroscope)
        {
            // Activer le gyroscope
            gyro = Input.gyro;
            gyro.enabled = true;

            // Enregistrer la rotation initiale du gyroscope
            initialRotation = Quaternion.Euler(0f, 0f, gyro.attitude.eulerAngles.z);
           
            Debug.Log("la rotation initiale est " + initialRotation.eulerAngles.z);
        }
        else
        {
            Debug.LogError("Gyroscope not supported on this device.");
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
 
        }

        

        //Debug.Log(rb.velocity);

        Vector2 v2 = rb.velocity.normalized;
        float f = Vector2.Angle(Vector2.right, v2);

        if (rb.velocity.x >= 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }


        // V�rifier si le gyroscope est activ�
        if (gyro != null && gyro.enabled)
        {
            // R�cup�rer l'angle d'Euler sur l'axe Z (yaw)
            float rotationZ = gyro.attitude.eulerAngles.z;

            // Appliquer la rotation horizontale au personnage
            Quaternion targetRotation = Quaternion.Inverse(initialRotation) * Quaternion.Euler(0f, 0f, rotationZ);
            //Quaternion playerRotation = Quaternion.Euler(0f, 0f, initialRotationz - targetRotation.z);
 
            transform.rotation = targetRotation;

           


        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pico"))
        {
            PlayerTakeDammage();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall" && GameManager.instance.ennemyDied == false)
        {
            wallSoundEffect.Play();
            Instantiate(fxWall, transform.position, Quaternion.identity);

            StartCoroutine(BoostVelocity(3));
            rb.AddForce(rb.velocity*50);
        }

        if (collision.collider.tag == "Bord")
        {
            //wallSoundEffect.Play();
        }

        if (collision.collider.tag == "Ennemy")
        {
            ennemiSoundEffect.Play();
            //collision.gameObject.GetComponent<Ennemy>().TakeDamage(1, this.gameObject);
        }
    }

    public void PlayerTakeDammage()
    {
        if (damageTaken == false)
        {
            GameManager.instance.healthPoint -= 1;
            Destroy(GameManager.instance.healthBar.transform.GetChild(GameManager.instance.healthPoint).gameObject);
            trapSoundEffect.Play();
            StartCoroutine(DammageTaken());
            StartCoroutine(DammageBlink());


        }
        
        if (GameManager.instance.healthPoint <= 0)
        {
            rb.Sleep();
            GetComponent<Collider2D>().enabled = false;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.parent = GameManager.instance.currentRoom.transform;
            animator.SetTrigger("die");

            GameManager.instance.blockInput = true;
            StartCoroutine(ReLoadScene());

            GameManager.instance.greySquare.SetTrigger("greytransition");


        }
    }

    public IEnumerator BoostVelocity(int i)
    {
        maxVelocity += i;
        yield return new WaitForSeconds(0.2f);
        maxVelocity -= i;
    }

   public IEnumerator DammageTaken()
    {
        damageTaken = true;
        yield return new WaitForSeconds(0.6f);
        damageTaken = false;
    }

    public IEnumerator DammageBlink()
    {
        for (int i = 0; i < 3; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    public IEnumerator ReLoadScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator WaitidleState(float i)
    {
        yield return new WaitForSeconds(i);
        rb.AddForce(GameManager.instance.currentRoom.GetComponent<Room>().pulse * speed);
        animator.SetTrigger("start");

    }
}
