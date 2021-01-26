using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Пул
    [System.Serializable]
    public class RarePool
    {
        public int tag;
        public GameObject prefab;
        public int size;
    }

public class RareBonusPool : MonoBehaviour
{
    public List<RarePool> RarePools; // список пулов
    public Dictionary<int, Queue<GameObject>> RarePoolDictionary; // словарь с обьектами пула

    void Awake()
    {
        RarePoolDictionary = new Dictionary<int, Queue<GameObject>>();

        foreach (RarePool pool in RarePools)
        {
            pool.size = Random.Range(1,10);
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(this.transform);
                objectPool.Enqueue(obj);
            }

            RarePoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromRarePool(int tag, Vector3 position, Quaternion rotation)
    {
        GameObject objectToSPawn = RarePoolDictionary[tag].Dequeue();

        objectToSPawn.SetActive(true);
        objectToSPawn.transform.position = position;
        objectToSPawn.transform.rotation = rotation;

        IPooledInterface pooledObj = objectToSPawn.GetComponent<IPooledInterface>();
        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        //poolDictionary[tag].Enqueue(objectToSPawn);
        return objectToSPawn;
    }
}
