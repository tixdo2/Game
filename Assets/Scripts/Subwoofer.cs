using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subwoofer : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !other.isTrigger)
        {
            Action(other.gameObject);
        }

    }
    
    void Action(GameObject Player)
    {
        Player.GetComponent<Rigidbody2D>().AddForce(transform.up * 20, ForceMode2D.Impulse);
    }

    void Update()
    {
        
    }
}
