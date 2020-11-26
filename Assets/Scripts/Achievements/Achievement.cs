using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SkinData", menuName = "Achievements/Achievement")]
[Serializable]
public class Achievement : ScriptableObject
{
    public string name;
    public List<int> count;
    public int done;
    public List<int> rewardsCoins;
    public int numberOfComplete = 0;
    public Skin rewardSkin;
    public Wallet wallet;
    public bool isDone;
    public bool isCoinsReward => numberOfComplete < rewardsCoins.Count;
    

    public delegate void Notification(Achievement achievement);
    public event Notification AchievementDone;

   
    
    public void Action()
    {
        if (isDone)
        {
            done = count.Last();
            return;

        }
        
        
        
        
        if(isCoinsReward)
            wallet.AddCoins(rewardsCoins[numberOfComplete]);
        else
            rewardSkin.isUnlock = true;
        
        if (count[numberOfComplete] == done)
        {
            AchievementDone?.Invoke(this); //an.Notification(this);
            numberOfComplete++;
        }
        
        
        if (numberOfComplete == count.Count)
        {
            
            isDone = true;
        }
    }
    
    
}

public enum REWARD
{
    SKIN,
    GOLD,
    DIAMOND
}



