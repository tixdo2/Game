using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SkinData", menuName = "Achievements/Achievement")]
[Serializable]
public class Achievement : ScriptableObject
{
    public string name;
    public int count;
    public int done;
    public REWARD rewardType;
    public Skin rewardSkin;
    public int rewardCoins;
    public int rewardDiamonds;
    public bool isDone;

    public AchievementNotification an;

    public delegate void Notification(Achievement achievement);
    public event Notification AchievementDone;

   
    
    public void Action()
    {
        if(done < count)
            return;
        
        
        rewardSkin.isUnlock = true;
        done = count;
        isDone = true;
        an.Notification(this);

    }
    
}

public enum REWARD
{
    SKIN,
    GOLD,
    DIAMOND
}



