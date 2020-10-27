using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playerGO;

    public Sprite skin;
    
    public int skinIndex;

    public TextMeshProUGUI ScoreTMP, ScoreDiedTMP;

    public GameObject conteinerGame, conteinerMenu, conteinerDead;

    public Animator AnimatorPause,AnimatorDead;

    private PlayerController PC;

    private Vector3 maxPosition;

    private float startPostionY;

    private int score, maxScore = 0;


    private bool isPause=false;

    private float timer;



    void Awake()
    {
        Time.timeScale = 1f;
        PC = playerGO.GetComponent<PlayerController>();

        startPostionY = PC.transform.position.y;
        maxPosition = PC.transform.position;
        PC.ChangeSkin(PlayerCustomizer.skin);
    }

    

    void Update()
    {
        if(PC.PI.isAlive)
        {
            
            if (score <= 0) ScoreTMP.SetText("0");
            
            score = Mathf.FloorToInt(PC.transform.position.y - startPostionY);
            
            
            if (maxScore < score)
            {
                
                maxScore = score;
            }

            if (maxPosition.y <  PC.transform.position.y)
            {
                maxPosition.y = PC.transform.position.y;
            }

            ScoreTMP.SetText(maxScore.ToString());
            
            

        }
        else
        {
            DeadMenu();
        }

        if ((maxPosition.y > (PC.transform.position.y + 20))) DeadMenu();



    }

    void DeadMenu()
    {

        conteinerDead.SetActive(true);
        ScoreDiedTMP.SetText(maxScore.ToString());
        Time.timeScale = 0;
        PC.PI.Score = score;

    }
    
    
    public void PauseMenu()
    {

        Debug.Log(isPause);

        if(isPause)
        {
            conteinerGame.SetActive(true);
            conteinerMenu.SetActive(false);
            //AnimatorPause.SetBool("IsPaused", false);
            timer = 1f;
            isPause=false;
        }
        else 
        {
            conteinerGame.SetActive(false);
            conteinerMenu.SetActive(true);
            //AnimatorPause.SetBool("IsPaused", true);

            timer = 0;
            isPause=true;
        }

        Time.timeScale = timer;

    }

    public void RestarGame()
    {
        Debug.Log("restarted");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    

}
