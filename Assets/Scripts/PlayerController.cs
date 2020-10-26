using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInfo PI;
    public HealthBar HB;

    public void Healing(float Count)
    {
        StartCoroutine(HealingWait(Count));
    }

    public void Damage(float Count)
    {
        StartCoroutine(DamageWait(Count));
    }

    //Изменения спрайта скина
    public void ChangeSkin(Sprite newSkin)
    {
        PI.skin = newSkin;
        PI.skinRender.sprite = PI.skin;
    }

    private IEnumerator HealingWait(float Count) 
    {
        for (float ft = 0f; ft < Count; ft += 1f) 
        {
            PI.HP += 1f;
            yield return new WaitForSeconds(3f * Time.deltaTime);
            HB.fill = PI.HP / 100;
            
        }
    }

    private IEnumerator DamageWait(float Count) 
    {
        for (float ft = 0f; ft < Count; ft += 1f) 
        {
            PI.HP -= 1f;
            yield return new WaitForSeconds(3f * Time.deltaTime);
            HB.fill = PI.HP / 100;
        }
    }      
}