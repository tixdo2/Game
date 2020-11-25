using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecorder : MonoBehaviour
{
    public float force=0.3f;
    public float thrust = 1.0f;
    public bool isWave=true;
    public  Collider2D col;

    void FixedUpdate()
    {
        StartCoroutine(Waves());
    }


    private IEnumerator Waves ()
    {
        yield return new WaitForSeconds(0.78f);   
        
        if (isWave)
        GetComponent<SpriteRenderer>().enabled = false;
        else col.enabled = false;

        yield return new WaitForSeconds(0.8f);  
        StopCoroutines(); 
    }

    private void StopCoroutines()
    {
        if (isWave)
        GetComponent<SpriteRenderer>().enabled = true;
        else col.enabled = true;
        StopAllCoroutines();
    } 

}
