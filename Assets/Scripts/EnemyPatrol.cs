using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {
        if (target.position.x > transform.position.x)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    
                   
                }
                else if (target.position.x < transform.position.x)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    
                   
                }
    }
}
