using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public TextMeshProUGUI ScoreTMP, ScoreDiedTMP, CoinsTMP,DeadCoinsTMP;

    public GameObject conteinerGame, conteinerMenu, conteinerDead;

    
    static public GameManager Game;
    

    [SerializeField]
    private GameObject maxScore;
    
    public PlayerController _pc;

    private GameObject _playerGO;

    private Vector3 _maxPosition;

    private float _startPostionY;
    
    private bool _isPause=false;

    private float _timer;
    
    private int _coins;

    private int _skinIndex = 0; // = PlayerPrefs.GetInt("skinIndex");


    private void Awake()
    {
        //if(Game!=null)
        Game = this;

    }
    private void Start()
    {
        Game = this;
        Time.timeScale = 1f;
        //Init();
        
    }

    public void InitPlayer(PlayerController playerController)
    {
        _pc = playerController;
        var position = _pc.transform.position;
        _startPostionY = position.y;
        _maxPosition = position;
    }
    
    private void Update()
    {
        if(_pc.PI.isAlive)
        {

            if (_pc.PI.Score <= 0)
            {
                _pc.PI.Score = 0;
                ScoreTMP.SetText("0");
            }

            

            if (_maxPosition.y <  _pc.transform.position.y)
            {

                _maxPosition.y = _pc.transform.position.y;
            }
            
            _pc.PI.Score = Mathf.FloorToInt(_maxPosition.y - _startPostionY);
            
            _pc.PI.Score += _pc.PI.BonusScore;
            
            ScoreTMP.SetText(_pc.PI.Score.ToString());
            CoinsTMP.SetText(_pc.PI.Coins.ToString());
        }
        else
        {
            DeadMenu();
        }

        if ((_maxPosition.y > (_pc.transform.position.y + 20)))
                DeadMenu();
        
    }

    private void DeadMenu()
    {
        conteinerDead.SetActive(true);
        ScoreDiedTMP.SetText(_pc.PI.Score.ToString());
        DeadCoinsTMP.SetText(_pc.PI.Coins.ToString());
        
        if (IsMaxScroe())
            maxScore.SetActive(true);

        Time.timeScale = 0;
    }

    private bool IsMaxScroe()
    {
        return _pc.PI.Score > DataManager.Data.MaxScore;
    }

    private void SetScore()
    {
        Debug.Log(DataManager.Data.MaxScore);
        int dataMaxScore = DataManager.Data.MaxScore;

        if (dataMaxScore < _pc.PI.Score)
            DataManager.Data.MaxScore = _pc.PI.Score;
        
    }

    private void SetCoins()
    {
        DataManager.Data.Wallet[0].AddCoins(_pc.PI.Coins);
    }
    
    public void PauseMenu()
    {
        if(_isPause)
        {
            conteinerGame.SetActive(true);
            conteinerMenu.SetActive(false);
            
            _timer = 1f;
            _isPause=false;
        }
        else 
        {
            conteinerGame.SetActive(false);
            conteinerMenu.SetActive(true);

            _timer = 0;
            _isPause=true;
        }

        Time.timeScale = _timer;
    }

    public void RestarGame()
    {
        SetScore();
        SetCoins();
        
        AchievementNotification.ANotification.UnsubEvents();
        
        DataManager.Data.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SetScore();
        SetCoins();
        
        AchievementNotification.ANotification.UnsubEvents();
        
        DataManager.Data.SaveData();
        SceneManager.LoadScene(0);
    }
}
