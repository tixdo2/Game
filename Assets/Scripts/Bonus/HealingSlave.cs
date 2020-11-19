using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSlave : MonoBehaviour
{
    public float Count = 20;
    [SerializeField] private List<Achievement> _achievements = new List<Achievement>();

    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !other.isTrigger)
        {
            Action(other.gameObject);
        }
    }

    private void Action(GameObject Player)
    {
        _achievements[0].done++;
        _achievements[0].Action();
        Player.GetComponent<PlayerController>().Healing(Count);
        gameObject.SetActive(false);
    }
    
    
}
