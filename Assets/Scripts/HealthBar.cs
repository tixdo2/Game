﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Bar;
    public float fill;

    void Start()
    {
        fill = 0.5f;
    }

    void Update()
    {
        Bar.fillAmount = fill;
    }
}
