using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "WalletData", menuName = "Customizer/Wallet")]
public class Wallet: ScriptableObject
{
    [SerializeField]
    private int coins;
    [SerializeField]
    private int diamonds;

    public int GetCoins() => coins;
    public int GetDiamonds() => diamonds;
    public void AddCoins(int value) => coins += value;
    public void SubCoins(int value) => coins -= value;
    public void AddDiamods(int value) => diamonds += value;
    public void SubDiamods(int value) => diamonds -= value;
}