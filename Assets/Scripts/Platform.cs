using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //данные об обьекте
    public float x; // координата x
    public float y; // координата y
    private string Tag; // Тэг обьекта
    public int size = 0; // Тип обьекта 1х 2х 3х
    public bool MoveControl = false; //Переменная для контроля движения платформы

    //движение
    private Vector3 movement = Vector3.left * 0.1f; // скорость движения влево-вправо 1х Платформы
    private float speed = 1f; // скорость движения платформ вниз

    void Start()
    {
        //Локальнаые данные обьекта на котором висит скрипт
        Tag = this.tag;

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

        //Будет ли платформа двигаться влево-вправо
        int ChanceForMove = Random.Range(1,21);
        if(size == 1)
        {
            if(ChanceForMove >=12 && ChanceForMove < 21)
                MoveControl = true;
        }
    }

    void FixedUpdate ()
    {
        //Постоянно изменяемые координаты платформы
        x = this.transform.position.x;
        y = this.transform.position.y;
        
        // Движение всех платформ
        transform.Translate(0, -(speed * Time.deltaTime), 0);

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
}
