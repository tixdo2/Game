using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float HP;
    public int Score;    
    public bool isAlive {get {return HP>0f;}}
    public Sprite skin;
    public SpriteRenderer skinRender;

    



    //Загрузка спрайтов
    void Awake()
    {
        skinRender.sprite = skin;

    }

}
