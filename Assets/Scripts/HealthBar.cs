using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform Bar;

    public void UpdateHealth(float x)
    {

        Bar.localPosition = new Vector2(Bar.localPosition.x + x, Bar.localPosition.y);

    }
    
}
