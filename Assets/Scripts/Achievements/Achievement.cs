using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "Achievements/Achievement")]
[Serializable]
public class Achievement : Achievements
{
    public override void Action()
    {
        if(done < count)
            return;
        
        //if(Event==null)
        //    Event = new AchievementsEvent();
        
        reward.isUnlock = true;
        done = count;
        isDone = true;
    }
    
}
