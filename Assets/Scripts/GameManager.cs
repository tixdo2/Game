using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerGO;

    public Sprite skin;
    public int skinIndex;

    public GameObject menuButton, conteinerMenu;

    private PlayerController PC;

    private bool isPause=false;

    private float timer;



    void Awake()
    {
        PC = playerGO.GetComponent<PlayerController>();

        Debug.Log(PlayerCustomizer.skin);
    
        PC.ChangeSkin(PlayerCustomizer.skin);
    }

    public void PauseMenu()
    {

        Debug.Log(isPause);

        if(isPause)
        {
            menuButton.SetActive(true);
            conteinerMenu.SetActive(false);
            timer = 1f;
            isPause=false;
        }
        else 
        {
            
            menuButton.SetActive(false);
            conteinerMenu.SetActive(true);
            timer = 0;
            isPause=true;
        }

        Time.timeScale = timer;

    }

}
