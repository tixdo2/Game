using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator Animator;

    public void Play()
    {
		SceneManager.LoadScene(1);
       //StartCoroutine("Play");
    }

	IEnumerator PlayC()
	{   
        Animator.SetBool("IsPlay", true);
        yield return new WaitForSeconds(0.5f);

		SceneManager.LoadScene(1);

		yield return null;
	}
 
}
