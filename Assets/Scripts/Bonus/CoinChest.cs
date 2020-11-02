using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinChest : MonoBehaviour, IPooledInterface
{
    public int CountCoins;
    public List<GameObject> Coins;
    private float x,y;
    private bool isCoinDrop = false;
    private GameObject Player;
    private int playerObject, collideObject;

    public void OnObjectSpawn()
    {
        Debug.Log("Chest");
        ObjectsPooler objPool = transform.parent.GetComponent<ObjectsPooler>();

        Coins = new List<GameObject>();
        playerObject=LayerMask.NameToLayer("Player");
        collideObject=LayerMask.NameToLayer("Bonus");

        int RandomXP = Random.Range(1, 101);
        
        if (RandomXP >= 1 && RandomXP <= 10)
            CountCoins = 6;
        else if(RandomXP > 10 && RandomXP <= 35)
            CountCoins = 4;
        else if(RandomXP > 35 && RandomXP <= 100)
            CountCoins = 2;
        
        x = transform.position.x;
        y = transform.position.y;

        for(int i=0; i<CountCoins; i++)
        {
            GameObject coin = objPool.SpawnFromPool("Coin", new Vector3(x, y+0.7f, -1), Quaternion.identity);
            coin.SetActive(false);
            Coins.Add(coin);
        }
    } 
    /*
    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().isGrounded = true;
        }
    }
    */

    public void OnTriggerEnter2D(Collider2D other)
    {
        // && !other.isTrigger
        if(other.tag == "Player" && !isCoinDrop)
        {
            
            Player = other.gameObject;
            isCoinDrop = true;
            StartCoroutine(CoinDrop());
        }
    }

    private IEnumerator CoinDrop() 
    {
        Debug.Log(1);
        foreach (GameObject drop in Coins)
        {
            int RandomWay = Random.Range(1,11);
            drop.SetActive(true);
            if(RandomWay >=1 && RandomWay <=5)
                drop.GetComponent<Rigidbody2D>().AddForce((transform.up + transform.right) *2, ForceMode2D.Impulse);
            else if(RandomWay >5 && RandomWay <=10)
                drop.GetComponent<Rigidbody2D>().AddForce((transform.up - transform.right) *2, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
        }

        //Physics2D.IgnoreLayerCollision(playerObject, collideObject, true);
        StopCoroutines();
    }

    void Update()
    {
        if(Player != null)
        {
            if(Coins.Count > 0)
            {
                for(int i = 0; i<Coins.Count; i++)
                {
                    if(Coins[i].transform.position.y < Player.transform.position.y - 5.5f)
                    {
                        Coins[i].SetActive(false);
                        Coins.Remove(Coins[0]);
                        //Physics2D.IgnoreLayerCollision(playerObject, collideObject, false);
                    }
                }
            }
        }
    }

    private void StopCoroutines()
    {
        StopAllCoroutines();
    } 
}
