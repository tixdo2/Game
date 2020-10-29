using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecorder : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        StartCoroutine(Waves());
    }

    private IEnumerator Waves ()
    {
        yield return new WaitForSeconds(0.5f);
                       
            //GameObject.SetActive(true);
            StopCoroutines(); 
    }

    private void StopCoroutines()
    {
        StopAllCoroutines();
        //GameObject.SetActive(false);
    } 

}
