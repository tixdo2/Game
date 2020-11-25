using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AchievementMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject tamplate;
    [SerializeField] private GameObject mainMenuConteiner;
    [SerializeField] private GameObject AchievementsConteiner;
    [SerializeField] private GameObject content;
    public List<Achievement> achievementses = new List<Achievement>();

    private DataManager _dataManager;
    
    public void GenerateMenu()
    {
        _dataManager = GetComponent<DataManager>();
        achievementses = _dataManager.Achievements;
        
        mainMenuConteiner.SetActive(false);
        AchievementsConteiner.SetActive(true);
        foreach (var ach in achievementses)
        {
            var go = Instantiate(tamplate, Vector3.zero, Quaternion.identity);
            var ta = go.GetComponent<TampleteAchievement>();
            ta.Achievement = ach;
            ta.AMM = this;
            ta.Name.SetText(ach.name);
            go.SetActive(true);
            go.transform.SetParent(content.transform);
            go.transform.localScale = new Vector3(1, 1, 1);

            
        }
        
    }

    public void CloseAchievementMenu()
    {
        mainMenuConteiner.SetActive(true);
        AchievementsConteiner.SetActive(false);
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }

    public void OpenAchievement(int index)
    {

        for (int i = index + 1; i < content.transform.childCount; i++)
        {
            content.transform.GetChild(i).DOLocalMoveY(content.transform.GetChild(i).transform.localPosition.y-180f * achievementses[index].count.Count-1  , .1f);
           
        }
        
    }

    public void CloseAchievement(int index)
    {
        for (int i = index + 1; i < content.transform.childCount; i++)
        {
            content.transform.GetChild(i).DOLocalMoveY(content.transform.GetChild(i).transform.localPosition.y+180f * achievementses[index].count.Count-1  , .1f);
           
        }
    }
    
}
