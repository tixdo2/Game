using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMobs : MonoBehaviour
{
    int HP;
    int attackPow;
    int speed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
/*
1. Патрульный
    Ходит от точки до точки
    не обращает внимания на игрока
    атакует при касании
2. Агрессор
    Патрулирует как и враг 1, но при виде игрока следует за ним
    ...
*/