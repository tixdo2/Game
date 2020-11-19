using System.Collections;
using System.Collections.Generic;
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
            go.SetActive(true);
            go.transform.SetParent(content.transform);
            go.transform.localScale = new Vector3(1, 1, 1);

            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ach.name);
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(ach.done.ToString());
            go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(ach.count.ToString());
            
            if(ach.isDone)
                go.transform.GetChild(3).gameObject.SetActive(true);
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
    
}
