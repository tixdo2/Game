using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private int playerObject, collideObject;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerObject=LayerMask.NameToLayer("Player");
        collideObject=LayerMask.NameToLayer("Bonus");
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
        Player.GetComponent<PlayerController>().GetCoin();
        gameObject.SetActive(false);
    }

    void Update()
    {
        Debug.Log(_rigidbody2D.velocity.y);
        if (_rigidbody2D.velocity.y!=0)
        {
            Physics2D.IgnoreLayerCollision(playerObject, collideObject, true);
        }
        else 
        {
            Physics2D.IgnoreLayerCollision(playerObject, collideObject, false);
        }
    }
}
