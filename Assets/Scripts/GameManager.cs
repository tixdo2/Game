using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerGO;

    public Sprite skin;
    public int skinIndex;

    private PlayerController PC;



    void Awake()
    {
        PC = playerGO.GetComponent<PlayerController>();

        Debug.Log(PlayerCustomizer.skin);
    
        PC.ChangeSkin(PlayerCustomizer.skin);
    }

}
