using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float HP;

    public int Coins = 0;   
    public int Score = 0;
    public int BonusScore = 0;
    public bool isAlive {get {return HP>0f;}}
    public Sprite skin;
    public SpriteRenderer skinRender;

    



    //Загрузка спрайтов
    void Awake()
    {
        skinRender.sprite = skin;

    }

}
