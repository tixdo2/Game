using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooler : MonoBehaviour
{
    // Пул
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools; // список пулов
    public Dictionary<string, Queue<GameObject>> poolDictionary; // словарь с обьектами пула

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(this.transform);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        GameObject objectToSPawn = poolDictionary[tag].Dequeue();

        objectToSPawn.SetActive(true);
        objectToSPawn.transform.position = position;
        objectToSPawn.transform.rotation = rotation;

        IPooledInterface pooledObj = objectToSPawn.GetComponent<IPooledInterface>();
        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSPawn);

        return objectToSPawn;
    }
}
