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
    private float roomRotationSpeed = 5f ;

    public int healthPoint = 5;

    public GameObject healthBar;

    private float maxHorizontalRoom = 100;
    private float minHorizontalRoom = -100;
    private float maxVerticalRoom = 100;
    private float minVerticalRoom = -100;


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

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        rbRoom.MoveRotation(rotationMovement);
            
    }


    

}
