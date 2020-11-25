using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TampleteAchievement : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI Name;
    
    public Achievement Achievement;

    public AchievementMenuManager AMM;

    [SerializeField] private GameObject achgo;

    [SerializeField] private Transform conteiner;

    private bool _isOpen;
    

    private void Start()
    {
        int i = 0;
        foreach (var item in Achievement.count)
        {
            var go = Instantiate(achgo);
            go.transform.SetParent(conteiner);
            go.transform.localScale = Vector3.one;
            
            var goa = go.GetComponent<AchievementChild>();
            
            string s = Achievement.done.ToString() + '/' + item.ToString();
            goa.Name.SetText(s);
  
            if (i < Achievement.rewardsCoins.Count)
                s = Achievement.rewardsCoins[i].ToString();
            else
                s = "SKIN";
            
            goa.Reward.SetText(s);
            goa.Check.SetActive(Achievement.done>item);
            goa.CoinIcon.SetActive(i < Achievement.rewardsCoins.Count);
            i++;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isOpen)
        {
            Open();
            AMM.OpenAchievement(transform.GetSiblingIndex());
        }
        else
        {
            Close();
            AMM.CloseAchievement(transform.GetSiblingIndex());
        }
    }

    private void Open()
    {

        conteiner.DOScale(1f, .1f).SetEase(Ease.InOutQuart);
        _isOpen = true;
    }

    private void Close()
    {
        conteiner.DOScale(0f, .1f).SetEase(Ease.InOutQuart);
        _isOpen = false;

    }
    
    
    
    
}
