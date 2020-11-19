using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


[Serializable]
public abstract class Achievements : ScriptableObject
{
        [SerializeField]public string name;
        [SerializeField]public int count;
        [SerializeField]public int done;
        [SerializeField]public Skin reward;
        [SerializeField]public bool isDone;
        
        public abstract void Action();
}


public class AchievementsEvent : UnityEvent<Achievements>{}


