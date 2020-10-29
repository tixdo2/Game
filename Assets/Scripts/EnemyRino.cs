using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRino : MonoBehaviour
{
    public float speed = 2f;
    public float HP = 100;

    public float rayDistance;
    public LayerMask NeedLayer;

    private bool movingRight = true;
    private Transform target;
    private Rigidbody2D rb;
    private bool follow;
    private bool ready=true;


    RaycastHit2D hit;
    RaycastHit2D back_hit;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(movingRight==true)
        {
            hit = Physics2D.Raycast(transform.position,transform.localScale.x*Vector2.right,rayDistance,NeedLayer);
            back_hit = Physics2D.Raycast(transform.position,transform.localScale.x*Vector2.left,rayDistance/2,NeedLayer);
        }
        else if(movingRight==false)
        {
            hit = Physics2D.Raycast(transform.position,transform.localScale.x*Vector2.left,rayDistance,NeedLayer);
            back_hit = Physics2D.Raycast(transform.position,transform.localScale.x*Vector2.right,rayDistance/2,NeedLayer);    
        }   
        if ((hit.collider!=null)||(back_hit.collider!=null)/*||(hit.collider.tag=="Player")||(back_hit.collider.tag=="Player")*/)
            {
                follow=true;
            }
            else 
            {
                follow=false;
                ready=true;
            }
        if (follow)
        {
            if (ready){ rb.AddForce(transform.right * -15, ForceMode2D.Impulse); ready=false;}
             if (target.position.x > transform.position.x)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight=true;
                    StartCoroutine(Run(-speed));
                }
                else if (target.position.x < transform.position.x)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight=false;
                    StartCoroutine(Run(speed));
                }
        }
        
    }

    private IEnumerator Run(float speedR)
    {
        
        yield return new WaitForSeconds(0.5f);
                       
            rb.AddForce(transform.right * 1000, ForceMode2D.Impulse);
            StopCoroutines();
            
    }

    private void StopCoroutines()
    {
        StopAllCoroutines();
    } 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(movingRight==true)
        {
            
            Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * rayDistance/2);
            Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * (rayDistance/4));
        }
        else if(movingRight==false)
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * rayDistance/2);
            Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * (rayDistance/4));

        }
    }
}
