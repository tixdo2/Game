using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Platform pl; //ссылка на скрипт Platform
    public SpawnController spwn;
    public List<GameObject> Platforms; // Список с платформами на сцене
    public GameObject Player;

    private float offset = 17; // смещение первой платформы при старте

    //Локальные данные для генерации обькта в нужных координатах
    private float x = -2.577f;
    private float y = -6.68f;
    private int type; // тип платформы

    // Префабы платформ
    public GameObject Plat3x; 
    public GameObject Plat2x;
    public GameObject Plat1x;

    public ObjectsPooler objPool;

    // Инициализация списка до старта
    void Awake()
    {
        Platforms = new List<GameObject>();
    }

    void Start()
    {
        //Генерация 9 платформ для старта 
        for(int i = 0;i<12;i++)
            SpawnPlat();
    }
  
    // Метод для контроля генерацит платформ
    public void SpawnPlat()
    {
        // Шанс генерации определенных платформ
        int RandomValue = Random.Range(1, 101);
        if (RandomValue <= 35)
            type = 1;
        else if(RandomValue > 35 && RandomValue <= 75) 
            type = 2;
        else if(RandomValue > 75 && RandomValue <= 100) 
            type = 3;
        
        // Генерация платформ в зависимости от выше рандомно-выбранного типа выше
        switch(type)
        {
            case 1:
                x = Random.Range(-2.57f, 2.58f); // Область по ширине, в которой будет генерироваться платформа
                y += 2.7f; // Расстояние между платформами
                GameObject go1 = objPool.SpawnFromPool("1xPlatform", new Vector3(x, y+offset, 0), Quaternion.identity); // Создание платформы 
                Platforms.Add(go1); // Добавление платформы в список платформ
                break;
            case 2:
                x = Random.Range(-3f, 3f);
                y += 2.7f;
                GameObject go2 = objPool.SpawnFromPool("2xPlatform", new Vector3(x, y+offset, 0), Quaternion.identity);
                Platforms.Add(go2);
                break;
            case 3:
                x = Random.Range(-3f, 3f);
                y += 2.7f;
                GameObject go3 = objPool.SpawnFromPool("3xPlatform", new Vector3(x, y+offset, 0), Quaternion.identity);
                Platforms.Add(go3);
                break;
        }

        if(!Platforms[Platforms.Count-1].GetComponent<Platform>().MoveControl)
        {
            spwn.RandomBonus(Platforms[Platforms.Count-1]);
        }
    }

    void Update()
    {
        // спавн новой платформы при удалении первой платформы
        if(Platforms[0] != null && Platforms[0].transform.position.y < Player.transform.position.y - 8.5f)
        {
            Platforms[0].SetActive(false);
            Platforms[0].GetComponent<Platform>().curBonus = false;
            foreach (Transform child in Platforms[0].transform)
            {
                child.gameObject.SetActive(true);
            }
            Platforms.Remove(Platforms[0]);
            SpawnPlat();
        }
    }
	
}
