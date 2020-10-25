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
    private GameObject Item;

    void Start()
    {
        
    } 
    public void SpawnBonus(GameObject platform)
    {
        int ItemToStawn = Random.Range(1, 31); // случайный выбор бонуса

            if (ItemToStawn <= 10)
                Item = Healing;
            else if(ItemToStawn > 10 && ItemToStawn <= 20)
                Item = Poison;
            else if(ItemToStawn > 20 && ItemToStawn <= 30)
                Item = JumpSub;

        int RandomBonusSpawn = Random.Range(1, 51); // случайное создание бонуса
        
            if (RandomBonusSpawn < 10)
                platform.GetComponent<Platform>().SpawnBonus(Item);
    }

    void Update()
    {

    }
}
