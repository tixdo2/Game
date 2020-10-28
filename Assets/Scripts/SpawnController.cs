using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    // Префабы бонусов
    public GameObject Healing;
    public GameObject Poison;
    public GameObject JumpSub;
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

                if (ItemToStawn <= 20)
                    Item = "Healing";
                else if(ItemToStawn > 20 && ItemToStawn <= 40)
                    Item = "Poison";
                else if(ItemToStawn > 40 && ItemToStawn <= 55)
                    Item = "Sub";
                else if(ItemToStawn > 55 && ItemToStawn <= 65)
                {     
                        if(platform.GetComponent<Platform>().size == 2 || platform.GetComponent<Platform>().size == 2)
                            Item = "Diplom"; 
                }else return;
                        
            int RandomBonusSpawn = Random.Range(1, 51); // случайное создание бонуса
            
                if (RandomBonusSpawn < 15)
                    SpawnBonus(Item, platform);
        }
    }

    // спавн бонуса на платформе
    public void SpawnBonus(string Item, GameObject platform)
    {
        float offset = 0f;
        if(Item == "Sub")  // смещение бонуса "Батут"
        {
            offset = 0.1f;
        }else if(Item == "Diplom") {offset = 0.56f;} else{offset = 0.488f;}

        platform.GetComponent<Platform>().curBonus = true;
        float x1 = platform.GetComponent<Platform>().StartPoint.position.x;
        float x2 = platform.GetComponent<Platform>().EndPoint.position.x;
        float xSpawn = Random.Range(x1, x2); // в случайной позиции на платформе
        
        Vector3 curPlace = new Vector3(xSpawn, platform.transform.position.y + offset, 0); // место где создается бонус
        GameObject Bonus = objPool.SpawnFromPool(Item, curPlace, Quaternion.identity); 
        bonus.Add(Bonus);

        if(platform.GetComponent<Platform>().MoveControl)
        {
            Bonus.transform.SetParent(platform.transform);
        }
        
    }

    void Update()
    {
        if(bonus.Count() > 0)
        {
            if(bonus[0].transform.position.y < Player.transform.position.y - 5.7f)
            {
                bonus[0].SetActive(false);
                bonus.Remove(bonus[0]);
            }
        }
    }
}
