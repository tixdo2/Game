using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinChest : MonoBehaviour, IPooledInterface
{
    public int CountItem;
    public List<GameObject> Items;
    private float x,y;
    public bool isCoinDrop = false;
    private GameObject Player;
    private int playerObject, collideObject;

    public ObjectsPooler objPool;
    public RareBonusPool RarePool;

    public void OnObjectSpawn()
    {
        objPool = transform.parent.GetComponent<ObjectsPooler>();
        RarePool = GameObject.FindObjectOfType<RareBonusPool>();

        Items = new List<GameObject>();

        int RandomXP = Random.Range(1, 101);
        
        if (RandomXP >= 1 && RandomXP <= 10)
            CountItem = 6;
        else if(RandomXP > 10 && RandomXP <= 35)
            CountItem = 4;
        else if(RandomXP > 35 && RandomXP <= 100)
            CountItem = 2;
        
        isCoinDrop = false;
    }

    public void Update()
    {
        x = transform.position.x;
        y = transform.position.y;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !other.isTrigger && !isCoinDrop)
        {
            Player = other.gameObject;
            isCoinDrop = true;
            Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
            StartCoroutine(CoinDrop());
        }
    }

    private IEnumerator CoinDrop() 
    {
        GetComponent<Animator>().SetBool("isOpen", true);
        yield return new WaitForSeconds(0.5f);

        
        for(int i=0; i<CountItem; i++)
        {
            int RandomWay = Random.Range(1,3);
            int RandomItem = Random.Range(1,101);
            GameObject Item;

            if(RandomItem <= 5)
            {
                int tag = Random.Range(1,3);
                Item = RarePool.SpawnFromRarePool(tag, new Vector3(x, y+0.7f, -1), Quaternion.identity);
                Item.SetActive(true);
                Items.Add(Item);
            }
            else
            {
                Item = objPool.SpawnFromPool("Coin", new Vector3(x, y+0.7f, -1), Quaternion.identity);
                Item.SetActive(true);
                Items.Add(Item);
            }

            if(RandomWay == 1)
                Item.GetComponent<Rigidbody2D>().AddForce((transform.up + transform.right) *2, ForceMode2D.Impulse);
            else if(RandomWay == 2)
                Item.GetComponent<Rigidbody2D>().AddForce((transform.up - transform.right) *2, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.2f);
        }

        StopCoroutines();
    }

    private void StopCoroutines()
    {
        StopAllCoroutines();
        GetComponent<Animator>().SetBool("isOpen", false); 
    } 
}