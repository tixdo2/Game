using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class AchievementNotification : MonoBehaviour
{
    [SerializeField] 
    private DataManager _dataManager;

    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Done;
    [SerializeField] private TextMeshProUGUI Count;
    [SerializeField] private GameObject isDone;

    
    
    private void Start()
    {
        InitEvents();
        DOTween.Init();
        DOTween.SetTweensCapacity(2000, 100);
        DOTween.defaultTimeScaleIndependent = true;
    }
    

    private void InitEvents()
    {
        foreach (var item in _dataManager.Achievements)
        {
            if(!item.isDone)
                item.AchievementDone += Notification;
        }
    }

    public void UnsubEvents()
    {
        foreach (var item in _dataManager.Achievements)
        {
            item.AchievementDone -= Notification;
        }
    }
    

    public void Notification(Achievement achievement)
    {
        Name.SetText(achievement.name);
        Done.SetText(achievement.done.ToString());
        Count.SetText(achievement.count[achievement.numberOfComplete].ToString());
        
        if(achievement.isDone)
            isDone.SetActive(true);
        
        if(achievement.isDone)
            achievement.AchievementDone -= Notification;
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
