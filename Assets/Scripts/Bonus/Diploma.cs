﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diploma : MonoBehaviour, IPooledInterface
{
    public int CountXP;
    [SerializeField] private List<Achievement> _achievements = new List<Achievement>();

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
        ParticleSystem Particle = Player.transform.GetChild(4).GetComponent<ParticleSystem>();
        Particle.Play();
        AchievementAction();
        Player.GetComponent<PlayerController>().GetXpFromDiplom(CountXP);
        gameObject.SetActive(false);
    }
    
    private void AchievementAction()
    {
        _achievements[0].done++;
        _achievements[0].Action();
    }
}
