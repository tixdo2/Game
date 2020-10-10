using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Platform pl;

    private float x = -2.577f;
    private float y = -6.68f;
    private int type;

    void Start()
    {
        pl.ClonePlatform(1, x, y);
        for(int i = 0;i<30;i++)
           SpawnPlat();
    }
  
    public void SpawnPlat()
    {
        x = Random.Range(-2.57f, 2.58f);
        y += 3f;
        type = Random.Range(1, 3);
        pl.ClonePlatform(type, x, y);
    }
}
