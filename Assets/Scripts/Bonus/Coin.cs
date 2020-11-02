using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !other.isTrigger)
        {
            Action(other.gameObject);
        }
    }

    void Action(GameObject Player)
    {
        Player.GetComponent<PlayerController>().GetCoin();
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
