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

    public TextMeshProUGUI ScoreTMP, ScoreDiedTMP, CoinsTMP,DeadCoinsTMP;

    public GameObject conteinerGame, conteinerMenu, conteinerDead;
    
    public HealthBar HealthBar;

    public Animator AnimatorPause,AnimatorDead;
    static public GameManager Instance;

    [SerializeField]
    private Wallet _wallet;

    [SerializeField] 
    private DataManager _dataManager;

    [SerializeField]
    private GameObject maxScore;
    
    private PlayerController _pc;

    private GameObject _playerGO;

    private Vector3 _maxPosition;

    private float _startPostionY;
    
    private bool _isPause=false;

    private float _timer;
    
    private int _coins;

    private int _skinIndex = 0; // = PlayerPrefs.GetInt("skinIndex");



    private void Start()
    {
        Time.timeScale = 1f;
        Instance = this;
        Init();
    }

    private void Init()
    {
        _skinIndex = _dataManager.SkinIndex;
        var position= new Vector3(0.8f, -5.663f, 0);
        
        _playerGO = Instantiate(Skins[_skinIndex], position, Quaternion.identity);
        _pc = _playerGO.GetComponent<PlayerController>();
        
        InitUI();
        InitContollers();
        
        
        _startPostionY = _pc.transform.position.y;
        _maxPosition = _pc.transform.position;
    }

    private void InitUI()
    {
        
        PlayerMovement playerMovement = _pc.GetComponent<PlayerMovement>();
        playerMovement.Button_l = Controllers[0];
        playerMovement.Button_R = Controllers[1];
        playerMovement.Button_J = Controllers[2];
        playerMovement.Button_A = Controllers[3];
        playerMovement.joystik = Controllers[4];
    }

    private void InitContollers()
    {
        SpawnController spawnController = GetComponent<SpawnController>();
        spawnController.Player = _playerGO;

        _pc.HB = HealthBar;

        PlatformController platformController = GetComponent<PlatformController>();
        platformController.Player = _playerGO;

        CameraController cameraController = GetComponent<CameraController>();
        cameraController.player = _playerGO.transform;
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
        else
            maxScore.SetActive(false);
        //SetScore();
        Time.timeScale = 0;
    }

    private bool IsMaxScroe()
    {
        return _pc.PI.Score > _dataManager.MaxScore;
    }

    private void SetScore()
    {
        Debug.Log(_dataManager.MaxScore);
        int maxScore = _dataManager.MaxScore;

        if (maxScore < _pc.PI.Score)
            _dataManager.MaxScore = _pc.PI.Score;
        
    }

    private void SetCoins()
    {
        _wallet.AddCoins(_pc.PI.Coins);
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
        _dataManager.SaveData();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SetScore();
        SetCoins();
        
        _dataManager.SaveData();

        SceneManager.LoadScene(0);
    }
}
