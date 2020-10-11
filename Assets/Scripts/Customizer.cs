using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizer : MonoBehaviour
{
    public PlayerController PC;

    public GameManager GM;

    public SpriteRenderer skinsR, headsR;


    public int skinsCount, headsCount;

    List<Sprite> skins, heads;

    public int skinIndex = 0, headIndex = 0; 

    void Awake()
    {

        
        skins = new List<Sprite>();

        heads = new List<Sprite>();

        skinIndex = GM.skinIndex;
        headIndex = GM.headIndex;

        
        

        skins.Add(Resources.Load<Sprite>("Sprites/Customized/Skins/MarlowStandart"));

        heads.Add(Resources.Load<Sprite>("Sprites/Customized/Heads/MarlowHeadStandart"));
        heads.Add(Resources.Load<Sprite>("Sprites/Customized/Heads/MarlowHead2"));

        skinsCount = skins.Count;
        headsCount = heads.Count;
        if(PlayerPrefs.GetInt("headIndex") == null && PlayerPrefs.GetInt("skinIndex") == null)
        {
            PlayerCustomizer.head = heads[0];
            PlayerCustomizer.skin = skins[0];
        }
        else
        {
            headIndex = PlayerPrefs.GetInt("headIndex");
            skinIndex = PlayerPrefs.GetInt("skinIndex");

            PlayerCustomizer.head = heads[headIndex];
            PlayerCustomizer.skin = skins[skinIndex];
        }

        headsR.sprite = heads[headIndex];
    }

    public void NextHead()
    {
        



        if(headIndex < headsCount-1)
        {
            headIndex++;
            headsR.sprite = heads[headIndex];
            return;

        }

        if(headIndex == headsCount-1)
        {
            headIndex = 0;
            headsR.sprite = heads[headIndex];

        }
 

    }

    public void PrevHead()
    {
        if(headIndex == 0) 
        {
            headIndex = headsCount-1;
            headsR.sprite = heads[headIndex];
            return;
        }
        
        if(headIndex >= 0)
        {
            headIndex--;
            headsR.sprite = heads[headIndex];
            return;
        }            
    }

    public void Accept()
    {
        ChangeHead();
        ChangeSkin();
        PlayerPrefs.Save();
    }

    void ChangeHead()
    {
        PlayerCustomizer.head = heads[headIndex];
        PlayerPrefs.SetInt("headIndex", headIndex);
    }

    void ChangeSkin()
    {
        PlayerCustomizer.skin = skins[skinIndex];
        PlayerPrefs.SetInt("skinIndex", skinIndex);
    }


    
}
