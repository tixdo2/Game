using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Test1 : Achievements
{
    
    public void Action()
    {
        if(done < count)
            return;
        Event.Invoke(this);
        isDone = true;
    }
}
