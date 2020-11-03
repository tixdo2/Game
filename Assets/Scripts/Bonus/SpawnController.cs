using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    // Префабы бонусов
    private string Item;

    public GameObject Player;
    public List<GameObject> bonus;

    public ObjectsPooler objPool;

    void Awake()
    {
        bonus = new List<GameObject>();
    } 

    public void RandomBonus(GameObject platform)
    {
        if(platform.GetComponent<Platform>().curBonus == false)
        {
            int ItemToStawn = Random.Range(1, 101); // случайный выбор бонуса

                if (ItemToStawn <= 10)
                    Item = "Healing";
                else if(ItemToStawn > 10 && ItemToStawn <= 65)
                    Item = "Poison";
                else if(ItemToStawn > 65 && ItemToStawn <= 80)
                    Item = "Sub";
                else if(ItemToStawn > 80 && ItemToStawn <= 90)
                {     
                        if(platform.GetComponent<Platform>().size == 2 || platform.GetComponent<Platform>().size == 3)
                            Item = "Diplom"; 
                }
                else if(ItemToStawn > 90 && ItemToStawn <= 100)
                    Item = "Chest";
                else return;
                        
            int RandomBonusSpawn = Random.Range(1, 101); // случайное создание бонуса
            
                if (RandomBonusSpawn < 60)
                    SpawnBonus(Item, platform);
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

    void Update()
    {
        if(bonus.Count > 0)
        {
            if(bonus[0].transform.position.y < Player.transform.position.y - 8f)
            {
                bonus[0].SetActive(false);
                bonus.Remove(bonus[0]);
            }
        }
    }
}
