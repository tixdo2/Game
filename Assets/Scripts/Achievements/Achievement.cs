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
    //public List<int> rewarsdDiamonds;
    public int numberOfComplete = 0;
    public Skin rewardSkin;
    public bool isDone;
    

    public delegate void Notification(Achievement achievement);
    public event Notification AchievementDone;

   
    
    public void Action()
    {
        if (isDone)
        {
            done = count.Last();
            return;

        }
        
        
        
        

        if (count[numberOfComplete] == done)
        {
            AchievementDone?.Invoke(this); //an.Notification(this);
            numberOfComplete++;
        }
        
        if (numberOfComplete == count.Count)
        {
            Debug.Log(111);
            rewardSkin.isUnlock = true;
            isDone = true;
        }
    }

    public bool isCoinsReward()
    {
        return numberOfComplete < rewardsCoins.Count;
    }
    
    
    
    
}

public enum REWARD
{
    SKIN,
    GOLD,
    DIAMOND
}



