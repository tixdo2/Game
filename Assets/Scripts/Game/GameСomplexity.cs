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
    private float PosToUP = 100f;

    [SerializeField]
    private float BonusSpwn, SpikesSpwn, MobsSpwn, SpikesBalance, SpeedBoost;

    void Awake()
    {
        SC = GetComponent<SpawnController>();
        PC = GetComponent<PlatformController>();
    }

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
        float camPos = transform.position.y;

        if(camPos >= PosToUP)
        {
            PosToUP+=100f;
            DifficultUp();
        }
    }
}
