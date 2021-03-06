﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewYearHat : MonoBehaviour
{
    //public float Count = 20;
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
        //ParticleSystem Particle = Player.transform.GetChild(1).GetComponent<ParticleSystem>();
        //Particle.Play();
        AchievementAction();  
        //Player.GetComponent<PlayerController>().Healing(Count);
        gameObject.SetActive(false);
    }

    private void AchievementAction()
    {
        _achievements[0].done++;
        _achievements[0].Action();
    }
}
