using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class AchievementNotification : MonoBehaviour
{
    public List<Achievement> Achievements;

    
    //private Transform notification;

    [SerializeField] 
    private DataManager _dataManager;
    
    private void Start()
    {
        InitAchievements();
        InitEvents();
        DOTween.Init();
        DOTween.SetTweensCapacity(2000, 100);
        DOTween.defaultTimeScaleIndependent = true;
    }

    private void InitAchievements()
    {
        Achievements = _dataManager.Achievements;
    }

    private void InitEvents()
    {
        foreach (var item in Achievements)
        {
            item.an = this;
        }
    }

    public void Notification(Achievement achievement)
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(achievement.name);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(achievement.done.ToString());
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(achievement.count.ToString());
        
        if(achievement.isDone)
            transform.GetChild(3).gameObject.SetActive(true);
        StartCoroutine(NotificatonAnim());
    }

    private IEnumerator NotificatonAnim()
    {
        transform.DOLocalMoveX(-50,  0.5f).SetEase(Ease.InOutQuart);
        yield return new WaitForSeconds(1f);
        transform.DOLocalMoveX(-1050, 0.5f).SetEase(Ease.InOutQuart);

        yield return null;
    }

    
}

public class AchievementEvent : UnityEvent<Achievement>
{}
