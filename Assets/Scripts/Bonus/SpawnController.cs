using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    // Префабы бонусов
    private string ItemBonus;
    private string ItemMobs;
    private string ItemSpikes;

    // игрок и списки обьектов
    public GameObject Player;
    public List<GameObject> bonus;
    public List<GameObject> mobs;
    public List<GameObject> spikes;

    // Шансы спавна обьектов для их последующего баланса
    public float RandHeal = 20, RandPoison = 75, RandSub = 85, RandDiplom = 95, RandChest = 100;
    public float RandSpikes1X = 40, RandSpikes2X = 80, RandSpikes3X = 100, BalanceForSpikes = 2;
    public float RandMobsSpwn = 0, RandSpikesSpwn = 0, RandBonusSpwn = 30;

    // пул обьектов для спавна
    public ObjectsPooler objPool;

    public static SpawnController Controller;

    // кеширование списков
    void Awake()
    {
        Controller = this;
        
        bonus = new List<GameObject>();
        mobs = new List<GameObject>();
        spikes = new List<GameObject>();
    } 

    // выбор случайного бонуса для спавна
    public void RandomBonus(GameObject platform)
    {
        if(platform.GetComponent<Platform>().curBonus == false)
        {
            int ItemToSpawn = Random.Range(1, 101);

                if (ItemToSpawn <= RandHeal)
                    ItemBonus = "Healing";
                else if(ItemToSpawn > RandHeal && ItemToSpawn <= RandPoison)
                    ItemBonus = "Poison";
                else if(ItemToSpawn > RandPoison && ItemToSpawn <= RandSub)
                    ItemBonus = "Sub";
                else if(ItemToSpawn > RandSub && ItemToSpawn <= RandDiplom)
                {     
                    ItemBonus = "Diplom"; 
                }
                else if(ItemToSpawn > RandDiplom && ItemToSpawn <= RandChest)
                    ItemBonus = "Chest";
                        
            int RandomBonusSpawn = Random.Range(1, 101);
            
                if (RandomBonusSpawn <= RandBonusSpwn)
                    SpawnBonus(ItemBonus, platform);
        }
    }

    // выбор случайного моба для спавна
    public void RandomMobs(GameObject platform)
    {
        if(platform.GetComponent<Platform>().size == 3 || platform.GetComponent<Platform>().size == 2)
        {
        int ItemToSpawn = Random.Range(1, 101);

            if (ItemToSpawn <= 100)
            {
                ItemMobs = "Recorder"; 
            }
                        
        int RandomMobsSpawn = Random.Range(1, 101);
            
            if (RandomMobsSpawn <= RandMobsSpwn)
                SpawnMobs(ItemMobs, platform);
        }
    }

    // выбор случайного шипа для спавна
    public void RandomSpikes(GameObject platform)
    {
        int ItemToSpawn = Random.Range(1, 101);

            if (ItemToSpawn <= RandSpikes1X)
                ItemSpikes = "1xSpikes";
            else if(ItemToSpawn > RandSpikes1X && ItemToSpawn <= RandSpikes2X)
                ItemSpikes = "2xSpikes";
            else if(ItemToSpawn > RandSpikes2X && ItemToSpawn <= RandSpikes3X)
                ItemSpikes = "3xSpikes";

        int RandomSpikesSpawn = Random.Range(1, 101);
            
            if (RandomSpikesSpawn <= RandSpikesSpwn)
                SpawnSpikes(ItemSpikes, platform);
    }

    // спавн бонуса на платформе
    public void SpawnBonus(string Item, GameObject platform)
    {
        float xSpawn = 0f;
        float offset = 0f;
        if(Item == "Sub")
            offset = 0.1f;
        else if(Item == "Diplom") 
            offset = 0.56f;
        else if(Item == "Chest") 
            offset = 0.4f;  
        else offset = 0.488f;

        platform.GetComponent<Platform>().curBonus = true;
        xSpawn = SpawnPointX(platform);
    
        Vector3 curPlace = new Vector3(xSpawn, platform.transform.position.y + offset, -1);
        GameObject Bonus = objPool.SpawnFromPool(Item, curPlace, Quaternion.identity); 
        bonus.Add(Bonus);

    }

    // спавн моба на платформе
    public void SpawnMobs(string Item, GameObject platform)
    {
        float offset = 0.4f;
        float xSpawn = 0f;

        platform.GetComponent<Platform>().curMobs = true;
        xSpawn = SpawnPointX(platform);
            
        Vector3 curPlace = new Vector3(xSpawn, platform.transform.position.y+offset, -1);
        GameObject Mob = objPool.SpawnFromPool(Item, curPlace, Quaternion.identity); 
        mobs.Add(Mob);
    }

    // спавн шипов на платформе
    public void SpawnSpikes(string Item, GameObject platform)
    {
        float offset = 0.4f;
        float xSpawn = 0f;

        platform.GetComponent<Platform>().curSpikes = true;
        xSpawn = SpawnPointX(platform);
            
        Vector3 curPlace = new Vector3(xSpawn, platform.transform.position.y+offset, -1);
        GameObject Spike = objPool.SpawnFromPool(Item, curPlace, Quaternion.identity); 
        spikes.Add(Spike);

        int ChanceForDoubleSpikes = Random.Range(1,101);
        if((ChanceForDoubleSpikes >=1 && ChanceForDoubleSpikes <=BalanceForSpikes) && platform.GetComponent<Platform>().size == 3)  RandomSpikes(platform);
    }

    // выбор случайной точки спавна
    public float SpawnPointX(GameObject platform)
    {
        float spawnPoint = 0f;
        int randomSpawnPoint = 0;
        bool isFind = false;

        void Find()
        {
            if(platform.transform.GetChild(randomSpawnPoint).gameObject.activeSelf)
                {
                    spawnPoint = platform.transform.GetChild(randomSpawnPoint).position.x;
                    platform.transform.GetChild(randomSpawnPoint).gameObject.SetActive(false);
                    isFind = true;
                }
        }

        if(platform.GetComponent<Platform>().size == 1)
        {
            while(!isFind)
            {
                randomSpawnPoint = Random.Range(1,3);
                Find();
            }
        }
        else if(platform.GetComponent<Platform>().size == 2)
        {
            while(!isFind)
            {
                randomSpawnPoint = Random.Range(2,7);
                Find();
            }
        }
        else if(platform.GetComponent<Platform>().size == 3)
        {
            while(!isFind)
            {
                randomSpawnPoint = Random.Range(3,9);
                Find();
            }
        }

        return spawnPoint; 
    }

    // проверки каждый фрейм
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
                    }
                }
                Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), bonus[0].GetComponent<Collider2D>(), false);
                bonus.Remove(bonus[0]);
            }
        }

        if(mobs.Count > 0)
        {
            if(mobs[0].transform.position.y < Player.transform.position.y - 8f)
            {
                mobs[0].SetActive(false);
                mobs.Remove(mobs[0]);
            }
        }

        if(spikes.Count > 0)
        {
            if(spikes[0].transform.position.y < Player.transform.position.y - 8f)
            {
                spikes[0].SetActive(false);
                spikes.Remove(spikes[0]);
            }
        }
    }
}


