﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Platform pl; //ссылка на скрипт Platform
    public List<GameObject> Platforms; // Список с платформами на сцене

    //Локальные данные для генерации обькта в нужных координатах
    private float x = -2.577f;
    private float y = -6.68f;
    private int type; // тип платформы

    // Префабы платформ
    public GameObject Plat3x; 
    public GameObject Plat2x;
    public GameObject Plat1x;

    // Инициализация списка до старта
    void Awake()
    {
        Platforms = new List<GameObject>();
    }

    void Start()
    {
        //Генерация 9 платформ для старта 
        for(int i = 0;i<8;i++)
            SpawnPlat();
        y-=1.83f; // погрешность при спавнах
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
                GameObject go1 = Instantiate(Plat1x, new Vector3(x, y, 0), Quaternion.identity); // Создание платформы 
                Platforms.Add(go1); // Добавление платформы в список платформ
                break;
            case 2:
                x = Random.Range(-2.79f, 0.31f);
                y += 3f;
                GameObject go2 = Instantiate(Plat2x, new Vector3(x, y, 0), Quaternion.identity);
                Platforms.Add(go2);
                break;
            case 3:
                x = -2.440343f;
                y += 3f;
                GameObject go3 = Instantiate(Plat3x, new Vector3(x, y, 0), Quaternion.identity);
                Platforms.Add(go3);
                break;
        }
    }

    void FixedUpdate()
    {
        // спавн новой платформы при удалении первой платформы
        if(Platforms[0].transform.position.y < - 8.48f)
        {
            Destroy(Platforms[0]);
            Platforms.Remove(Platforms[0]);
            y-=3f; // погрешность при спавнах
            SpawnPlat();
        }
    }
}
