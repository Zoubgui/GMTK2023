using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    private float rotationMovement = 1;
    Vector3 rotation;

    // Start is called before the first frame update


    // Update is called once per frame
    

    private void FixedUpdate()
    {
        rotation = new Vector3(0, 0, rotationSpeed);
        transform.Rotate(rotation * Time.fixedDeltaTime);
    }
}
