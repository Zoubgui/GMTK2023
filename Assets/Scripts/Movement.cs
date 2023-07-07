using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update

    public float maxVelocity;
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
    }

    
}
