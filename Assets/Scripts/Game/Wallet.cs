using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalletData", menuName = "Customizer/Wallet")]
[Serializable]
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
    public void AddDiamonds(int value) => diamonds += value;
    public void SubDiamonds(int value) => diamonds -= value;

    public void SetCoins(int value) => coins = value;
    public void SetDiamonds(int value) => diamonds = value;
}