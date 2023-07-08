using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject gameBoy;
    public float timeOffset;
    public Vector3 posOffSet;
    private Vector3 velocity;

    private void Start()
    {
        gameBoy = GameManager.instance.currentRoom;
    }

    private void Update()
    {


        //Debug.Log(GameManager.instance.currentRoom.transform.rotation.z);

        Debug.Log(GameManager.instance.currentRoom.transform.rotation.eulerAngles); // en 0 ->  360

        float f = GameManager.instance.currentRoom.transform.rotation.z * 3.15f;
        posOffSet.y = f;
        transform.position = Vector3.SmoothDamp(transform.position, gameBoy.transform.position + posOffSet, ref velocity, timeOffset);
    }
}
