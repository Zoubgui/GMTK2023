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

    void Start()
    {
        rb.AddForce(GameManager.instance.currentRoom.GetComponent<Room>().pulse * speed);

        wallSoundEffect = transform.GetChild(1).GetComponent<AudioSource>();
        Debug.Log(wallSoundEffect);
        trapSoundEffect = transform.GetChild(2).GetComponent<AudioSource>();
        ennemiSoundEffect = transform.GetChild(3).GetComponent<AudioSource>();

        animator.SetTrigger("start");

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
        if (collision.collider.tag == "Wall")
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
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
