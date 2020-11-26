using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class AchievementNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Name;
    public static AchievementNotification ANotification;
    private void Awake()
    {
        ANotification = this;
    }
    
    private void Start()
    {
        InitEvents();
        DOTween.Init();
        DOTween.SetTweensCapacity(2000, 100);
        DOTween.defaultTimeScaleIndependent = true;
    }
    

    private void InitEvents()
    {
        foreach (var item in DataManager.Data.Achievements)
        {
            if(!item.isDone)
                item.AchievementDone += Notification;
        }
    }

    public void UnsubEvents()
    {
        foreach (var item in DataManager.Data.Achievements)
        {
            item.AchievementDone -= Notification;
        }
    }
    

    public void Notification(Achievement achievement)
    {
        
        Name.SetText(achievement.name);

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
