using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private Resolution CurrentResolution;
    private Rect SaveArea;
    private Rect Cutouts;
    private void Awake()
    {
        CurrentResolution = Screen.currentResolution;
        SaveArea = Screen.safeArea;

        var cutouts = Screen.cutouts;

    }
    
}
