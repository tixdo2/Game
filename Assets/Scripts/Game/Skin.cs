using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu(fileName = "SkinData", menuName = "Customizer/Skin")]
public class Skin: ScriptableObject
{
    public Currency currency;
    public GameObject prefab;
    public bool isBuying;
    public int cost;
}