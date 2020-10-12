using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Platform pl; //ссылка на скрипт Platform

    //Локальные данные для генерации обькта в нужных координатах
    private float x = -2.577f;
    private float y = -6.68f;
    private int type; // тип платформы

    void Start()
    {
        //Генерация 30 платформ для старта
        pl.ClonePlatform(1, x, y); // Генерация первой платформы 
        for(int i = 0;i<30;i++)
           SpawnPlat();
    }
  
    // Метод для контроля генерацит платформ
    public void SpawnPlat()
    {
        // Шанс генерации определенных платформ
        int RandomValue = Random.Range(1, 51);
        if (RandomValue < 25)
            type = 1;
        else if(RandomValue >= 25 && RandomValue < 45) 
            type = 2;
        else if(RandomValue >= 45 && RandomValue < 50) 
            type = 3;

        // Генерация платформ в зависимости от выше рандомно-выбранного типа выше
        switch(type)
        {
            case 1:
                x = Random.Range(-2.57f, 2.58f); // Область по ширине, в которой будет генерироваться платформа
                y += 3f; // Расстояние между платформами
                pl.ClonePlatform(type, x, y); // Передача параметров в скрипт Platform в метод ClonePLatform
                break;
            case 2:
                x = Random.Range(-2.79f, 0.31f);
                y += 3f;
                pl.ClonePlatform(type, x, y);
                break;
            case 3:
                x = -2.440343f;
                y += 3f;
                pl.ClonePlatform(type, x, y);
                break;
        }
    }

    void Update()
    {
          
    }
}
