using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Skins;

    public List<GameObject> Controllers;
    
    public Sprite skin;

    public int skinIndex;

    public TextMeshProUGUI ScoreTMP, ScoreDiedTMP, CoinsTMP;

    public GameObject conteinerGame, conteinerMenu, conteinerDead;
    
    public HealthBar HealthBar;

    public Animator AnimatorPause,AnimatorDead;

    [SerializeField]
    private Wallet _wallet;
    
    private PlayerController _pc;

    private GameObject _playerGO;

    private Vector3 _maxPosition;

    private float _startPostionY;
    
    private bool _isPause=false;

    private float _timer;
    
    private int _coins;

    private int _skinIndex = 0; // = PlayerPrefs.GetInt("skinIndex");



    void Awake()
    {
        Time.timeScale = 1f;
        
        Init();
    }

    void Init()
    {
       
        _skinIndex = PlayerPrefs.GetInt("skinIndex");
        Debug.Log("skinIndex "+_skinIndex);
        var position= new Vector3(0.8f, -5.663f, 0);
        
        _playerGO = Instantiate(Skins[_skinIndex], position, Quaternion.identity);
        _pc = _playerGO.GetComponent<PlayerController>();
        
        InitUI();
        InitContollers();
        
        
        _startPostionY = _pc.transform.position.y;
        _maxPosition = _pc.transform.position;
        //_pc.ChangeSkin(PlayerCustomizer.skin);
    }

    void InitUI()
    {
        PlayerMovement playerMovement = _pc.GetComponent<PlayerMovement>();
        playerMovement.Button_l = Controllers[0];
        playerMovement.Button_R = Controllers[1];
        playerMovement.Button_J = Controllers[2];
        playerMovement.Button_A = Controllers[3];
        playerMovement.joystik = Controllers[4];
    }

    void InitContollers()
    {
        SpawnController spawnController = GetComponent<SpawnController>();
        spawnController.Player = _playerGO;

        _pc.HB = HealthBar;

        PlatformController platformController = GetComponent<PlatformController>();
        platformController.Player = _playerGO;

        CameraController cameraController = GetComponent<CameraController>();
        cameraController.player = _playerGO.transform;
    }
    

    void Update()
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

        if ((_maxPosition.y > (_pc.transform.position.y + 20))) DeadMenu();



    }

    void DeadMenu()
    {
        
        
        conteinerDead.SetActive(true);
        
        
        Time.timeScale = 0;

    }

    void SetScore()
    {
        int maxScore = PlayerPrefs.GetInt("MaxScore");

        if(maxScore< _pc.PI.Score)
            PlayerPrefs.SetInt("MaxScore", _pc.PI.Score);
        ScoreDiedTMP.SetText(_pc.PI.Score.ToString());
    }

    void SetCoins()
    {

        _wallet.AddCoins(_pc.PI.Coins);
        // Debug.Log(PlayerPrefs.GetInt("Coins"));
        // int coins = PlayerPrefs.GetInt("Coins") + _pc.PI.Coins;
        
        // PlayerPrefs.SetInt("Coins", coins);
    }
    
    
    public void PauseMenu()
    {

        Debug.Log(_isPause);

        if(_isPause)
        {
            conteinerGame.SetActive(true);
            conteinerMenu.SetActive(false);
            //AnimatorPause.SetBool("IsPaused", false);
            _timer = 1f;
            _isPause=false;
        }
        else 
        {
            conteinerGame.SetActive(false);
            conteinerMenu.SetActive(true);
            //AnimatorPause.SetBool("IsPaused", true);

            _timer = 0;
            _isPause=true;
        }

        Time.timeScale = _timer;

    }

    public void RestarGame()
    {
        Debug.Log("restarted");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SetScore();
        SetCoins();
        SceneManager.LoadScene(0);
    }
    
    

}
