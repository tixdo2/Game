using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    // Префабы бонусов
    private string ItemBonus;
    private string ItemMobs;

    public GameObject Player;
    public List<GameObject> bonus;
    public List<GameObject> mobs;

    public ObjectsPooler objPool;

    void Awake()
    {
        bonus = new List<GameObject>();
        mobs = new List<GameObject>();
    } 

    public void RandomBonus(GameObject platform)
    {
        if(platform.GetComponent<Platform>().curBonus == false)
        {
            int ItemToStawn = Random.Range(1, 101); // случайный выбор бонуса

                if (ItemToStawn <= 20)
                    ItemBonus = "Healing";
                else if(ItemToStawn > 20 && ItemToStawn <= 75)
                    ItemBonus = "Poison";
                else if(ItemToStawn > 75 && ItemToStawn <= 85)
                    ItemBonus = "Sub";
                else if(ItemToStawn > 85 && ItemToStawn <= 95)
                {     
                        if(platform.GetComponent<Platform>().size == 2 || platform.GetComponent<Platform>().size == 3)
                            ItemBonus = "Diplom"; 
                }
                else if(ItemToStawn > 95 && ItemToStawn <= 100)
                    ItemBonus = "Chest";
                        
            int RandomBonusSpawn = Random.Range(1, 101); // случайное создание бонуса
            
                if (RandomBonusSpawn < 60)
                    SpawnBonus(ItemBonus, platform);
        }
    }

    public void RandomMobs(GameObject platform)
    {
        if(platform.GetComponent<Platform>().curMobs == false)
        {
            if(platform.GetComponent<Platform>().size == 3)
            {
            int ItemToStawn = Random.Range(1, 101); // случайный выбор моба

                if (ItemToStawn <= 100)
                {
                    ItemMobs = "Recorder"; 
                }
                        
            int RandomBonusSpawn = Random.Range(1, 101); // случайное создание моба
            
                if (RandomBonusSpawn <= 40)
                    SpawnMobs(ItemMobs, platform);
            }
        }
    }

    // спавн бонуса на платформе
    public void SpawnBonus(string Item, GameObject platform)
    {
        float xSpawn = 0f;
        float offset = 0f;
        if(Item == "Sub")  // смещение бонуса 
            offset = 0.1f;
        else if(Item == "Diplom") 
            offset = 0.56f;
        else if(Item == "Chest") 
            offset = 0.4f;  
        else offset = 0.488f;

        platform.GetComponent<Platform>().curBonus = true;
        float x1 = platform.GetComponent<Platform>().StartPoint.position.x;
        float x2 = platform.GetComponent<Platform>().EndPoint.position.x;
        if(Item == "Chest")
        {
            xSpawn = (x1+x2)/2;
        }
        else
        {
            xSpawn = Random.Range(x1, x2); // в случайной позиции на платформе
        } 
        
        Vector3 curPlace = new Vector3(xSpawn, platform.transform.position.y + offset, -1); // место где создается бонус
        GameObject Bonus = objPool.SpawnFromPool(Item, curPlace, Quaternion.identity); 
        bonus.Add(Bonus);

    }

    // спавн моба на платформе
    public void SpawnMobs(string Item, GameObject platform)
    {
        if(!platform.GetComponent<Platform>().curBonus)
        {
            float offset = 0.4f;
            float xSpawn = 0f;

            platform.GetComponent<Platform>().curMobs = true;
            float x1 = platform.GetComponent<Platform>().StartPoint.position.x;
            float x2 = platform.GetComponent<Platform>().EndPoint.position.x;
    
            xSpawn = Random.Range(x1, x2); // в случайной позиции на платформе
            
            Vector3 curPlace = new Vector3(xSpawn, platform.transform.position.y+offset, -1); // место где создается бонус
            GameObject Mob = objPool.SpawnFromPool(Item, curPlace, Quaternion.identity); 
            mobs.Add(Mob);
        }
    }

    void Update()
    {
        if(bonus.Count > 0)
        {
            if(bonus[0].transform.position.y < Player.transform.position.y - 8f)
            {
                bonus[0].SetActive(false);
                bonus[0].TryGetComponent<CoinChest>(out var coinchest); 
                if(coinchest&&coinchest.isCoinDrop)
                {
                    foreach(GameObject drop in coinchest.Coins)
                    {
                        
                        drop.SetActive(false);
                        coinchest.Coins.Remove(drop);
                        
                    }
                }
                Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), bonus[0].GetComponent<Collider2D>(), false);
                bonus.Remove(bonus[0]);
            }
        }

        if(mobs.Count > 0)
        {
            Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), mobs[0].GetComponent<Collider2D>(), true);
            if(mobs[0].transform.position.y < Player.transform.position.y - 8f)
            {
                mobs[0].SetActive(false);
                mobs.Remove(mobs[0]);
            }
        }
    }
}


