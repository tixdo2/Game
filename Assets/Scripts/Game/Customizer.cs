﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customizer : MonoBehaviour
{
    public PlayerController PC;

    public GameManager GM;

    public Image skinsR;


    public int skinsCount;

    List<Sprite> skins;

    public int skinIndex = 0;

    void Awake()
    {
        
        skins = new List<Sprite>();

        skinIndex = GM.skinIndex;

        skins.Add(Resources.Load<Sprite>("Sprites/Customized/Skins/Marlow"));
        skins.Add(Resources.Load<Sprite>("Sprites/Customized/Skins/MarlowFriend"));
        skins.Add(Resources.Load<Sprite>("Sprites/Customized/Skins/Marlow3"));

        skinsCount = skins.Count;

        if(PlayerPrefs.GetInt("skinIndex") == null)
        {
            PlayerCustomizer.skin = skins[0];
            skinsR.sprite = skins[0];
        }
        else
        {
            skinIndex = PlayerPrefs.GetInt("skinIndex");

            PlayerCustomizer.skin = skins[skinIndex];
            skinsR.sprite = skins[skinIndex];
        }
    }

    public void Next()
    {

        Debug.Log(1);
        if(skinIndex < skinsCount-1)
        {
            skinIndex++;
            skinsR.sprite = skins[skinIndex];
            return;

        }

        if(skinIndex == skinsCount-1)
        {
            skinIndex = 0;
            skinsR.sprite = skins[skinIndex];
            return;
        }
    }

    public void Prev()
    {
        Debug.Log(0);

        if(skinIndex == 0) 
        {
            skinIndex = skinsCount-1;
            skinsR.sprite = skins[skinIndex];
            return;
        }
        
        if(skinIndex >= 0)
        {
            skinIndex--;
            skinsR.sprite = skins[skinIndex];
            return;
        }            
    }

    public void Accept()
    {
        ChangeSkin();
        PlayerPrefs.Save();
    }

    void ChangeSkin()
    {
        PlayerCustomizer.skin = skins[skinIndex];
        PlayerPrefs.SetInt("skinIndex", skinIndex);
    }


    
}