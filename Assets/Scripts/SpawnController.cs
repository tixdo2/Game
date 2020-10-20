using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    public GameObject Bonus;

    void Start()
    {
        
    } 
    public void SpawnBonus(GameObject platform)
    {
        int RandomBonusSpawn = Random.Range(1, 51);
        if (RandomBonusSpawn < 20)
            platform.GetComponent<Platform>().SpawnBonus(Bonus);
        else if(RandomBonusSpawn >= 20 && RandomBonusSpawn <= 50) 
            return; 
    }

    void Update()
    {

    }
}
