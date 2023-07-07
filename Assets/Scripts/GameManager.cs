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
    [SerializeField] float maxVerticalRoom;
    [SerializeField] float minVerticalRoom;

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        //if (currentRoom.transform.position.x > minHorizontalRoom && currentRoom.transform.position.x < maxHorizontalRoom)
        //{
           
        //}
        //else
        //{
        //    movement.x = 0;
        //    movement.y = 0;
        //    Input.ResetInputAxes();
        //}



    }

    private void FixedUpdate()
    {
        ClampRoom();
       

    }

    private void ClampRoom()
    {
        float clampedX = Mathf.Clamp(currentRoom.transform.position.x, minHorizontalRoom, maxHorizontalRoom);
        float clampedY = Mathf.Clamp(currentRoom.transform.position.y, minVerticalRoom, maxVerticalRoom);
        Vector2 clampedPosition = new Vector2(clampedX, clampedY);
        rbRoom.MovePosition(clampedPosition + movement * roomSpeed * Time.fixedDeltaTime);
    }


}
