using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class SaveManager:MonoBehaviour
{
   public  Wallet _wallet;
   public  List<Skin> _skins;
   public  List<Achievement> _achievements;
   
   public   void SaveAllData(DataManager data)
   {
      Save save = new Save(data);
      
      Debug.Log(Application.persistentDataPath);
      
      //Debug.Log("SAVE");

      SaveData(save, "data");
   }

   public   void LoadAllData(DataManager data)
   {
      Save save = new Save();
      Debug.Log(Application.persistentDataPath);

      //Debug.Log("LOAD");
      
      LoadData(save, "data");
      
      save.Load(data);
   }
   
   
   private   void SaveData<T>(T data, string fileName)
   {
      string direction = Application.persistentDataPath + "/SaveData/";// + fileName + ".json";
      
      if (!Directory.Exists(direction))
         Directory.CreateDirectory(direction);

      var json = JsonUtility.ToJson(data);
      
      //Debug.Log(json);
      
      File.WriteAllText(direction+fileName+".json", json);
   }
   
   private   void LoadData<T>(T data, string fileName)
   {
      string path = Application.persistentDataPath + "/SaveData/" + fileName + ".json";

      if (File.Exists(path))
      {
         string json = File.ReadAllText(path);
         JsonUtility.FromJsonOverwrite(json, data);
      }
      else
      {
         Debug.Log("Save file does don`t exist");
      }
   }
}

[System.Serializable]
public class Save
{
   [System.Serializable]
   public struct skin
   {
      public bool isUnlock;

      public skin(Skin skin)
      {
         this.isUnlock = skin.isUnlock;
      }
      
   }

   [System.Serializable]
   public struct wallet
   {
      public int coins, diamonds;

      public wallet(Wallet wallet)
      {
         this.coins = wallet.GetCoins();
         this.diamonds = wallet.GetDiamonds();
      }
   }

   [System.Serializable]
   public struct achievement
   {
      public int done;
      public bool isDone;
      public int numberOfComplete;

      public achievement(Achievement achievement)
      {
         this.done = achievement.done;
         this.isDone = achievement.isDone;
         this.numberOfComplete = achievement.numberOfComplete;
      }
   }

   public List<skin> _skins = new List<skin>();
   public List<achievement> _achievements = new List<achievement>();
   public wallet _wallet;
   public int _skinIndex;
   public int _maxScore;

   public Save(){}
   public Save(DataManager data)
   {
      _skinIndex = data.SkinIndex;
      _maxScore = data.MaxScore;
      foreach (var item in data.Skins)
      {
         skin s = new skin(item);
         _skins.Add(s);

      }
      
      foreach (var item in data.Achievements)
      {
         achievement a = new achievement(item);
         _achievements.Add(a);
      }
   
      _wallet = new wallet(data.Wallet[0]);

   }

   public void Load(DataManager data)
   {

      data.SkinIndex = _skinIndex;
      data.MaxScore = _maxScore;
      int i = 0;
      
      foreach (var item in _skins)
      {
         data.Skins[i].isUnlock = item.isUnlock;
         i++;
      }

      i = 0;

      foreach (var item in _achievements)
      {
         data.Achievements[i].done = item.done;
         data.Achievements[i].isDone = item.isDone;
         data.Achievements[i].numberOfComplete = item.numberOfComplete;
         i++;
      }
      
      data.Wallet[0].SetCoins(_wallet.coins);
      data.Wallet[0].SetDiamonds(_wallet.diamonds);
   }
}
