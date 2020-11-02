﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IPooledInterface
{
    //данные об обьекте
    public float x; // координата x
    public float y; // координата y
    private string Tag; // Тэг обьекта
    public int size = 0; // Тип обьекта 1х 2х 3х
    public bool MoveControl = false; //Переменная для контроля движения платформы
    private bool Broken = false;

    public List<GameObject> Children = new List<GameObject>(); // список дочерних элементов у каждой платформы

    private Vector3 movement = Vector3.left; // скорость движения влево-вправо 1х Платформы

    public bool curBonus = false; // текущий бонус на платформе

    public Sprite BrokeLeft, BrokeMiddle, BrokeRight; // спрайты поломаных частей платформы
    public Sprite Left, Middle, Right;

    public Transform StartPoint, EndPoint; // Промежуток в котором создаются обьекты

    public void OnObjectSpawn()
    {
        //Локальнаые данные обьекта на котором висит скрипт
        Tag = this.tag;

        // Применение переменной size по Тегу размер
        switch(Tag)
        {
            case "1":
                size = 1;
                break;
            case "2":
                size = 2;
                break;
            case "3":
                size = 3;
                break;
        }

        // Добавление в список детей родителя
        foreach (Transform child in transform)
        {
            Children.Add(child.gameObject);
        }

        // Случайная генерация сломаных дочерних элементов платформы
        if(!Broken)
        {
            switch(size)
            {
                case 2:
                    int ChanceForDestroy2x = Random.Range(1, 101); // Весы
                    if(ChanceForDestroy2x >=1 && ChanceForDestroy2x < 11)
                    {
                        Children[0].GetComponent<ChildPlatform>().isBroke = true; // Делаем переменную в дочернем элементе активной
                        Children[0].GetComponent<SpriteRenderer>().sprite = BrokeRight; // Меняем спрайт
                        Broken = true;
                    }
                    else if(ChanceForDestroy2x >=11 && ChanceForDestroy2x < 21)
                    {
                        Children[1].GetComponent<ChildPlatform>().isBroke = true;
                        Children[1].GetComponent<SpriteRenderer>().sprite = BrokeLeft;
                        Broken = true;
                    }
                    break;
                case 3:
                    int ChanceForDestroy3x = Random.Range(1,101);
                    if(ChanceForDestroy3x >=1 && ChanceForDestroy3x < 11)
                    {
                        Children[0].GetComponent<ChildPlatform>().isBroke = true;
                        Children[0].GetComponent<SpriteRenderer>().sprite = BrokeLeft;
                        Broken = true;
                    }
                    else if(ChanceForDestroy3x >=11 && ChanceForDestroy3x < 21)
                    {
                        Children[1].GetComponent<ChildPlatform>().isBroke = true;
                        Children[1].GetComponent<SpriteRenderer>().sprite = BrokeMiddle;
                        Broken = true;
                    }
                    else if(ChanceForDestroy3x >=21 && ChanceForDestroy3x < 31)
                    {
                        Children[2].GetComponent<ChildPlatform>().isBroke = true;
                        Children[2].GetComponent<SpriteRenderer>().sprite = BrokeRight;
                        Broken = true;
                    }
                    break;
                }
            }else if(Broken)
            {
                foreach (Transform child in transform)
                {
                    if(child.GetComponent<SpriteRenderer>().sprite == BrokeRight)
                    {
                        child.GetComponent<SpriteRenderer>().sprite = Right;
                    }
                    else if(child.GetComponent<SpriteRenderer>().sprite == BrokeMiddle)
                    {
                        child.GetComponent<SpriteRenderer>().sprite = Middle;
                    }
                    else if(child.GetComponent<SpriteRenderer>().sprite == BrokeLeft)
                    {
                        child.GetComponent<SpriteRenderer>().sprite = Left;
                    }
                    break;
                }
            }
        
        //Будет ли платформа двигаться влево-вправо
        int ChanceForMove = Random.Range(1,101);
        if(size == 1)
        {
            if(ChanceForMove >=1 && ChanceForMove < 21)
                MoveControl = true;
        }
    }
    void FixedUpdate()
    {
        // Движение 1х платформы влево-вправо
        if(size==1 && MoveControl)
        {
            if (this.transform.position.x > 2.55f)
                movement = Vector3.left * 0.05f;
            else if (this.transform.position.x < -2.55f)
                movement = Vector3.right * 0.05f;
            this.transform.Translate(movement);
        }
    }
    void Update ()
    {
        //Постоянно изменяемые координаты платформы
        x = this.transform.position.x;
        y = this.transform.position.y;
    }
}
