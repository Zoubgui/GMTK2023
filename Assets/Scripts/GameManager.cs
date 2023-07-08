using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject currentRoom;

    private Vector2 translationMovement;
    private float rotationMovement = 0;
    public Rigidbody2D rbRoom;
    [SerializeField] float roomTranslationSpeed;
    [SerializeField] float roomRotationSpeed;

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
        translationMovement.x = Input.GetAxisRaw("Horizontal");
        translationMovement.y = Input.GetAxisRaw("Vertical");

        ClampedRotationRoom();

    }

    private void FixedUpdate()
    {

        ClampedTranslationRoom();
        

    }

    private void ClampedTranslationRoom()
    {
        float clampedX = Mathf.Clamp(currentRoom.transform.position.x, minHorizontalRoom, maxHorizontalRoom);
        float clampedY = Mathf.Clamp(currentRoom.transform.position.y, minVerticalRoom, maxVerticalRoom);
        Vector2 clampedPosition = new Vector2(clampedX, clampedY);
        rbRoom.MovePosition(clampedPosition + translationMovement * roomTranslationSpeed * Time.fixedDeltaTime);
    }

    private void ClampedRotationRoom()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationMovement += roomRotationSpeed * Time.fixedDeltaTime;
            
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            rotationMovement -= roomRotationSpeed * Time.fixedDeltaTime;
         
        }

        rbRoom.MoveRotation(rotationMovement);

    }

    }
