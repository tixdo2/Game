using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPlatform : MonoBehaviour
{
    public bool isBroke = false; // Переменная которая отвечает за сломанную платформу

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
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
