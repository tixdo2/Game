using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IPooledInterface
{
    
    private Rigidbody2D _rigidbody2D;
    
    private int _playerObject, _collideObject;

    public void OnObjectSpawn()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerObject=LayerMask.NameToLayer("Player");
        _collideObject=LayerMask.NameToLayer("Bonus");
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !other.isTrigger)
        {
            Action(other.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.TryGetComponent<Coin>(out var a); 
        if(a)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),a.GetComponent<Collider2D>(),true);
        }
    }

    void Action(GameObject Player)
    {
        Player.GetComponent<PlayerController>().GetCoin();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (_rigidbody2D.velocity.y!=0)
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, true);
            //Physics2D.IgnoreLayerCollision(_collideObject, _collideObject, true);
        }
        else 
        {
            Physics2D.IgnoreLayerCollision(_playerObject, _collideObject, false);
            //Physics2D.IgnoreLayerCollision(_collideObject, _collideObject, false);
        }
    }
}
