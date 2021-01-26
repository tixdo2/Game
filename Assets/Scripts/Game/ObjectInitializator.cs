using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInitializator : MonoBehaviour
{
    [InspectorName("PlayerPrefabs")]
    public List<GameObject> Skins;
    
    
    [Space(5)]
    
    public List<GameObject> Controllers;

    public HealthBar HealthBar;

    [Space(5)] 
    
    [SerializeField]
    private PlatformController _platformController;

    [SerializeField]
    private CameraController _cameraController;
    
    [SerializeField]
    private GameСomplexity _gameComplexity;
    
    

    private GameObject _playerGO;
    private PlayerController _playerController;


    private int _skinIndex;

    private void Start()
    {
        Init();
    }
    
    private void Init()
    {
        InitPlayer();
        InitUI();
        InitContollers();
    }

    private void InitPlayer()
    {
        _skinIndex = DataManager.Data.SkinIndex;

        var position= new Vector3(0.8f, -5.663f, 0);
        _playerGO = Instantiate(Skins[_skinIndex], position, Quaternion.identity);
        _playerController = _playerGO.GetComponent<PlayerController>();
        GameManager.Game.InitPlayer(_playerController);
    }

    private void InitUI()
    {
        PlayerMovement playerMovement = _playerController.GetComponent<PlayerMovement>();
        playerMovement.Button_J = Controllers[0];
        playerMovement.joystik = Controllers[1];
    }

    private void InitContollers()
    {
        
        SpawnController.Controller.Player = _playerGO;

        _gameComplexity.Player = _playerController;
        
        _gameComplexity.SC = SpawnController.Controller;
        _gameComplexity.PC = SpawnController.Controller.GetComponent<PlatformController>();

        _playerController.HB = HealthBar;
        
        _platformController.Player = _playerGO;
        
        _cameraController.player = _playerGO.transform;
    }
}
