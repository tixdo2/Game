using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecorder : MonoBehaviour
{
    public float force=0.3f;
    public float thrust = 1.0f;

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        StartCoroutine(Waves());
    }


    private IEnumerator Waves ()
    {
        yield return new WaitForSeconds(0.78f);   
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(0.8f);  
        StopCoroutines(); 
    }

    private void StopCoroutines()
    {
        GetComponent<Collider2D>().enabled =true;
        GetComponent<SpriteRenderer>().enabled = true;
        //GetComponent<Animator>().enabled = true;
        StopAllCoroutines();
    } 

    public void OnTriggerStay2D(Collider2D col)
    {
        if(!col.gameObject.GetComponent<PlayerMovement>().onSub)
            col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * thrust;
        //col.gameObject.GetComponent<PlayerMovement>().knockback();
        //col.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up*force, ForceMode2D.Impulse);
    }

}
