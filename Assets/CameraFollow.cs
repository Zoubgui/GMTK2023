using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject gameBoy;
    private float timeOffset = 0.7f;
    private Vector3 posOffSet = new Vector3(0,0,-10f);
    private Vector3 velocity;

    private void Start()
    {
        gameBoy = GameManager.instance.currentRoom;
    }

    private void Update()
    {


        //Debug.Log(GameManager.instance.currentRoom.transform.rotation.z);

        float a = GameManager.instance.currentRoom.transform.rotation.eulerAngles.z;
        float b = a / 360;
        float c = Mathf.Cos(b * 2 *Mathf.PI);
        float d = c * 3.15f;

    




        posOffSet.y = d;
        transform.position = Vector3.SmoothDamp(transform.position, gameBoy.transform.position + posOffSet, ref velocity, timeOffset);
    }
}
