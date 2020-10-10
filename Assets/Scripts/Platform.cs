using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float x;
    public float y;
    private string Tag; 
    public int size = 0;
    public bool isDestroy = false;

    private float speed = 1f;
    
    public GameObject Plat3x;
    public GameObject Plat2x;
    public GameObject Plat1x;

    void Start()
    {
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
    }

    void Update ()
    {
        x = this.transform.position.x;
        y = this.transform.position.y;

        //transform.Translate(0, -(speed * Time.deltaTime), 0);

        if(y < -7.48f )
        {
            Destroy(gameObject);
            isDestroy = true;
        } 
        
        // if(size==1)
        // {
        //     transform.position = new Vector3(transform.position.x, 1 + Mathf.Sin(Time.fixedTime) * 1, transform.position.z); плавное движение вверх-вниз
        // }
    }

    public void ClonePlatform(int size, float x, float y)
    {
        switch (size)
        {
            case 1:
                Instantiate(Plat1x, new Vector3(x, y, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(Plat2x, new Vector3(x, y, 0), Quaternion.identity);
                break;
            case 3:
                Instantiate(Plat3x, new Vector3(x, y, 0), Quaternion.identity);
                break;
        }
    }
}
