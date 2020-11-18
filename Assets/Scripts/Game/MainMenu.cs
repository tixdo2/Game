using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Game
{
	public class MainMenu : MonoBehaviour
	{
		//public Animator Animator;
		public GameObject BuyingMenu, ConteinerMenu;
		[SerializeField] private Customizer customizer;
		
		private void Start()
		{
			//PlayerPrefs.SetInt("Coins", 0);
			//CoinsTMP.SetText(PlayerPrefs.GetInt("Coins").ToString());
		}

		public void Play()
		{
			//Debug.Log(_customizer.ActiveSkin.isBuying);
			if (!customizer.ActiveSkin.isBuying)
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
			BuyingMenu.SetActive(false);
			ConteinerMenu.SetActive(true);
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
