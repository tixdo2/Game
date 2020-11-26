using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SkinData", menuName = "Customizer/Skin")]
[Serializable]
public class Skin : ScriptableObject
{
    [SerializeField] public Currency currency;
    [SerializeField] public GameObject prefab;
    [SerializeField] public bool isUnlock;
    [SerializeField] public bool isAchievement;
    [SerializeField] public int cost;
    [SerializeField] public Color color;
}