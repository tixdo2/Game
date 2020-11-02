using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPlatform : MonoBehaviour
{
    public bool isBroke = false; // Переменная которая отвечает за сломанную платформу
    public int child;
    public int size;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && isBroke)
        {
            StartCoroutine(Wait());
        }
    }

    // Задержка перед удалением обькта
    private IEnumerator Wait()
    {
        Animator anim = transform.parent.GetComponent<Animator>();
        switch (child)
            {
                case 1: 
                    anim.SetBool("StayBrokeLeft",false);
                    anim.SetBool("BrokeLeft",true);
                    
                    yield return new WaitForSeconds(0.5f);
                    
                    anim.SetBool("BrokeLeft",false);
                    anim.SetBool("IsBrokeLeft",true);
                    break;
                case 2: 
                    anim.SetBool("StayBrokeMiddle",false);
                    anim.SetBool("BrokeMiddle",true);
                    
                    yield return new WaitForSeconds(0.5f);
                    
                    anim.SetBool("BrokeMiddle",false);
                    anim.SetBool("IsBrokeMiddle",true);
                    break;
                case 3: 
                    anim.SetBool("StayBrokeRight",false);
                    anim.SetBool("BrokeRight",true);
                    
                    yield return new WaitForSeconds(0.5f);
                    
                    anim.SetBool("BrokeRight",false);
                    anim.SetBool("IsBrokeRight",true);
                    break;
            }
    }
        //gameObject.SetActive(false);
    

    // Вынос игрока из дочерних объектов
    private void OnCollisionExit2D(Collision2D collision)
    {                                           
        if (collision.gameObject.tag=="Player") 
        {
            collision.transform.SetParent(null);
        }   
    }
}
