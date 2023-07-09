using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject currentRoom;

    private Vector2 translationMovement;
    private float rotationMovement = 0;
    public Rigidbody2D rbRoom;
    private float roomTranslationSpeed = 2.5f;
    private float roomRotationSpeed = 50f ;

    public int healthPoint = 4;

    public GameObject healthBar;

    private float maxHorizontalRoom = 100;
    private float minHorizontalRoom = -100;
    private float maxVerticalRoom = 100;
    private float minVerticalRoom = -100;

    public bool blockInput;
    public Animator greySquare;




    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        greySquare = GameObject.Find("GreySquare").GetComponent<Animator>();
       
    }

    private void FixedUpdate()
    {

        translationMovement.x = Input.GetAxisRaw("Horizontal");
        translationMovement.y = Input.GetAxisRaw("Vertical");

        if (blockInput == false)
            ClampedTranslationRoom();

        if (blockInput == false)
            ClampedRotationRoom();
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

        
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        rbRoom.MoveRotation(rotationMovement);
            
    }


    

}
