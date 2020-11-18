using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSlave : MonoBehaviour
{
    public float Count = 20;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !other.isTrigger)
        {
            Action(other.gameObject);
        }
    }

    void Action(GameObject Player)
    {
        ParticleSystem Particle = Player.transform.GetChild(1).GetComponent<ParticleSystem>();
        Particle.Play();
        Player.GetComponent<PlayerController>().Healing(Count);
        gameObject.SetActive(false);
    }
}
