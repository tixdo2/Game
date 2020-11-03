using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSlave : MonoBehaviour
{
    public float Count = 20;

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("[eqweqwe]");
        if(other.tag == "Player" && !other.isTrigger)
        {
            //Debug.Log("HILOCHKA");
            Action(other.gameObject);
        }
    }

    void Action(GameObject Player)
    {
        Player.GetComponent<PlayerController>().Damage(Count);
        gameObject.SetActive(false);
    }
}
