using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInfo PI;

    //Изменения спрайта скина
    public void ChangeSkin(Sprite newSkin)
    {
        PI.skin = newSkin;
        PI.skinRender.sprite = PI.skin;
    }
}