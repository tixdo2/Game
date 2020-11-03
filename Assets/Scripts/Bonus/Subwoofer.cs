using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subwoofer : MonoBehaviour
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
        Player.GetComponent<Rigidbody2D>().AddForce(transform.up * 25, ForceMode2D.Impulse);
        Player.transform.SetParent(null);
        Player.GetComponent<PlayerMovement>().isGrounded = false;
    }

}
