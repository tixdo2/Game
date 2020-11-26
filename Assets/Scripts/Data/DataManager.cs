using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<Skin> Skins = new List<Skin>();
    public List<Achievement> Achievements= new List<Achievement>();
    public List<Wallet> Wallet = new List<Wallet>();
    public int SkinIndex;
    public int MaxScore;
    [SerializeField] private SaveManager _saveManager;
    public static DataManager Data;

    private void Awake()
    {
        //SaveData();
        LoadData();
        //if(Data!=null)
        Data = this;
    }
    
    private void Start()
    {   
        Data = this;
    }
    

    public void SaveData()
    {
        _saveManager.SaveAllData(this);
    }

    public void LoadData()
    {
        _saveManager.LoadAllData(this);
    }
}
