using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    public float maxVelocity;
    
    public SpriteRenderer sprite;

    AudioSource wallSoundEffect;
    AudioSource trapSoundEffect;
    AudioSource ennemiSoundEffect;

    public GameObject fxWall;
    public GameObject fxMort;

    void Start()
    {
        rb.AddForce(Vector2.down * speed/2);
        rb.AddForce(Vector2.left * speed/3);

        wallSoundEffect = transform.GetChild(1).GetComponent<AudioSource>();
        Debug.Log(wallSoundEffect);
        trapSoundEffect = transform.GetChild(2).GetComponent<AudioSource>();
        ennemiSoundEffect = transform.GetChild(3).GetComponent<AudioSource>();


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
            trapSoundEffect.Play();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            wallSoundEffect.Play();
            Instantiate(fxWall, transform.position, Quaternion.identity);
        }

        if (collision.collider.tag == "Ennemy")
        {
            ennemiSoundEffect.Play();
            collision.gameObject.GetComponent<Ennemy>().TakeDamage(1);
        }
    }

    public void PlayerTakeDammage()
    {
        GameManager.instance.healthPoint -= 1;
        Destroy(GameManager.instance.healthBar.transform.GetChild(GameManager.instance.healthPoint).gameObject);

        if (GameManager.instance.healthPoint <= 0)
        {
            GameObject _fxMort = Instantiate(fxMort, transform.position, Quaternion.identity);
            _fxMort.transform.parent = transform;
            rb.Sleep();
            GetComponent<Collider2D>().enabled = false;
            sprite.enabled = false;
        }
    }



}
