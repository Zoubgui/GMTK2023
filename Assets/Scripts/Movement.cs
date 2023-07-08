using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    public float maxVelocity;

    public Transform sprite;

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
        //Vector2.Angle(Vector2.right, v2);
        //Debug.Log(Vector2.Angle(Vector2.right, v2));
        //sprite.eulerAngles.Set(0,0, Vector2.Angle(Vector2.right, v2));

        //sprite.Rotate(0, 0, Vector2.Angle(Vector2.right, v2));
        //sprite.transform.rotation.Set(0,0,f,1);

        //sprite.rotation.SetEulerAngles(0f, 0f, f);
        Debug.Log(f);
        //sprite.localRotation.Set(0, 0, f, 1);
        sprite.rotation.Set(0, 0, f, 1);
    }

    
}
