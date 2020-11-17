using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


[Serializable]
public abstract class Achievements : ScriptableObject
{
        public string name;
        public int count;
        public int done;
        public Skin reward;
        public bool isDone;
        public AchievementsEvent Event;
        public void Action()
        {
                
        }
}


public class AchievementsEvent : UnityEvent<Achievements>{}


