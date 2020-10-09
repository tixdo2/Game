using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOperations : MonoBehaviour
{
    public PlayerInfo PI;

    //Изменения спрайта скина
    public void ChangeSkin(Sprite newSkin)
    {
        PI.skin = newSkin;
        PI.skinRender.sprite = PI.skin;
    }

    //Изменения спрайта головы
    public void ChangeHead(Sprite newHead)
    {
        PI.head = newHead;
        PI.headRender.sprite = newHead;
    }
}
