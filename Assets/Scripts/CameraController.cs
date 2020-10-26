using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //public float smooth= 5.0f;
    //public Vector3 offset = new Vector3 (0, 2, -5);
    public Transform player;

    void  Update ()
    {
        transform.position = new Vector3(0f, player.transform.position.y+4, -10f);
        //transform.position = Vector3.Lerp (transform.position, player.position + offset, Time.deltaTime * smooth);
    } 
}
