using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float CountDMG = 20f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Player" && !other.isTrigger)
        {
           
             Action(other.gameObject);
        }
    }

    private void Action(GameObject Player)
    {
        //Vector3 PlayerBack = new Vector3(Player.transform.position.x, Player.transform.position.x, 0f);
        Player.GetComponent<PlayerController>().Damage(CountDMG);
        Player.GetComponent<Rigidbody2D>().AddForce((transform.up + transform.right) *6, ForceMode2D.Impulse);
    }
}
