using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinChest : MonoBehaviour
{
    public int CountXP;

    public void OnObjectSpawn()
    {
        int RandomXP = Random.Range(1, 101);

        if (RandomXP >= 1 && RandomXP <= 10)
            CountXP = 100;
        else if(RandomXP > 10 && RandomXP <= 35)
            CountXP = 50;
        else if(RandomXP > 35 && RandomXP <= 100)
            CountXP = 25;
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
        Player.GetComponent<PlayerController>().GetXpFromDiplom(CountXP);
        gameObject.SetActive(false);
    }
}
