using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float HP = 50f;
    public int score;    

    public Sprite skin;
    public SpriteRenderer skinRender;


    //Загрузка спрайтов
    void Awake()
    {
        skinRender.sprite = skin;

    }

}
