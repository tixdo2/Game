using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameСomplexity : MonoBehaviour
{
    public Platform PL;
    public PlayerController Player;
    public SpawnController SC;
    public PlatformController PC;

    private float PlayerHP;
    private bool HealChanceUp = false;
    private float PosToUP = 50f;

    [SerializeField]
    private float BonusSpwn, SpikesSpwn, MobsSpwn, SpikesBalance, SpeedBoost;

    public void DifficultUp()
    {
        SC.RandMobsSpwn += MobsSpwn;
        SC.RandBonusSpwn += BonusSpwn;
        SC.BalanceForSpikes += SpikesBalance;
        SC.RandSpikesSpwn += SpikesSpwn;
        PC.MovementBoost += SpeedBoost;
    }

    void Update()
    {
        float PlayerPos = Player.gameObject.transform.position.y;
        if(PlayerPos >= PosToUP)
        {
            PosToUP+=50f;
            DifficultUp();
        }
    }
}
