using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Game
{
	public class MainMenu : MonoBehaviour
	{
		//public Animator Animator;
		public GameObject BuyingMenu, ConteinerMenu;

		public List<Achievements> Achievements;
		[SerializeField] private Customizer customizer;
		[SerializeField]private SaveManager _saveManager;

		public void Play()
		{
			//Debug.Log(_customizer.ActiveSkin.isBuying);
			if (!customizer.ActiveSkin.isUnlock)
				customizer.skinIndex = 0;
			
			
			SceneManager.LoadScene(1);
			//StartCoroutine("Play");
		}

		public void OpenBuyMenu()
		{
			BuyingMenu.SetActive(true);
			ConteinerMenu.SetActive(false);
		}

		public void CloseBuyMenu()
		{
			customizer.Accept();
			BuyingMenu.SetActive(false);
			ConteinerMenu.SetActive(true);
		}

		public void OpenAchievementMenu()
		{
			
		}

		public void CloseAchievementMenu()
		{
			
		}
		
		

		private IEnumerator PlayC()
		{   
			//Animator.SetBool("IsPlay", true);
			yield return new WaitForSeconds(0.5f);

			SceneManager.LoadScene(1);

			yield return null;
		}
		
		
 
	}
}
