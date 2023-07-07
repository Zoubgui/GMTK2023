using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject currentRoom;

    private Vector2 movement;
    public Rigidbody2D rbRoom;
    [SerializeField] float roomSpeed;

    [SerializeField] float maxHorizontalRoom;
    [SerializeField] float minHorizontalRoom;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (currentRoom.transform.position.x > minHorizontalRoom && currentRoom.transform.position.x < maxHorizontalRoom)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
            Input.ResetInputAxes();
        }



    }

    private void FixedUpdate()
    {
        rbRoom.MovePosition(rbRoom.position + movement * roomSpeed * Time.fixedDeltaTime);
        //Mathf.Clamp(currentRoom.transform.position.x, minHorizontalRoom, maxHorizontalRoom);
    }


}
