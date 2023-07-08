using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    public float maxVelocity;
    
    public SpriteRenderer sprite;

    public GameObject fxWall;
    public GameObject fxMort;

    void Start()
    {
        rb.AddForce(Vector2.down * speed/2);
        rb.AddForce(Vector2.left * speed/3);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(fxWall, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pico"))
        {
            PlayerTakeDammage();
        }
    }

    public void PlayerTakeDammage()
    {
        GameManager.instance.healthPoint -= 1;
        Destroy(GameManager.instance.healthBar.transform.GetChild(GameManager.instance.healthPoint).gameObject);

        if(GameManager.instance.healthPoint <= 0)
        {
            GameObject _fxMort = Instantiate(fxMort, transform.position, Quaternion.identity);
            _fxMort.transform.parent = transform;
            rb.Sleep();
            GetComponent<Collider2D>().enabled = false;
            sprite.enabled = false;
        }
    }

}
