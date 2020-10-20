using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBonus : MonoBehaviour
{
    public float x {get; set;}
    public float y {get; set;}

    protected abstract void Destroy();
    protected abstract void Action();
    protected abstract void Spawn();
}
