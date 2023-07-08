using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlateforemScript : MonoBehaviour
{
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;
    public float speed = 1f; // Vitesse de transition
   

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float temps = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, temps);


    }

    
}
