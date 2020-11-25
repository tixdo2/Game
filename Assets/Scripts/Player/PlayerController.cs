using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInfo PI;
    public HealthBar HB;
    public GameManager GM;

    private bool _bonuseffect;

    public void Healing(float Count)
    {
        StartCoroutine(HealingWait(Count));
    }

    public void Damage(float Count)
    {
        StartCoroutine(DamageWait(Count));
    }

    public void GetXpFromDiplom(int CountXP)
    {
        PI.BonusScore += CountXP;
    }

    public void GetCoin()
    {
        PI.Coins +=1;
    }

    //Изменения спрайта скина
    public void ChangeSkin(Sprite newSkin)
    {
        //PI.skin = newSkin;
        //PI.skinRender.sprite = PI.skin;
    }

    private IEnumerator HealingWait(float Count) 
    {        
        for (float ft = 0f; ft < Count; ft += 1f) 
        {
            if(PI.HP<100f)
            {
                PI.HP += 1f;
                HB.UpdateHealth(-2.65f);
                yield return new WaitForSeconds(3f * Time.deltaTime);
            }
            else
            {
                break;
                yield return null;  
            }
        }
    }

    private IEnumerator DamageWait(float Count) 
    {
        for (float ft = 0f; ft < Count; ft += 1f) 
        {
            if(PI.isAlive)
            {
                PI.HP -= 1f;
                HB.UpdateHealth(2.65f);
                yield return new WaitForSeconds(3f * Time.deltaTime);
            }

        }
    } 
}