using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSlave : MonoBehaviour
{
    
    public float Count = 20;
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
        Player.GetComponent<PlayerController>().Damage(Count);
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
