using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateGeneration : MonoBehaviour
{
    public List<GameObject> Plates = new List<GameObject>();
    public GameObject Plate1x;  //одинарная плита
    public GameObject Plate2x;  //двойная плита
    public GameObject Plate3x;  //тройная плита

    public GameObject cam;
    public GameObject canvas;

    void Start ()
    {
        Plates.Add(Plate1x);
        Plates.Add(Plate2x);
        Plates.Add(Plate3x);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(0);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(1);
            GameObject clone = Instantiate(Plates[Random.Range(0,3)], new Vector2(Random.Range(cam.transform.position.x + 3f, cam.transform.position.x + 6f), Random.Range(cam.transform.position.y + 13f, cam.transform.position.y + 14f)), Quaternion.identity) as GameObject;
            clone.transform.SetParent(canvas.transform, false);
        }
    }

    
    void Update()
    {
        
    }
}
