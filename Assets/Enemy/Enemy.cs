using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int moneyReward = 25;
    [SerializeField] int moneyPenalty = 25;

    Bank bank;

    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void DepositBank()
    {
        if (bank == null) return;
        bank.Deposit(moneyReward);
    }

    public void WithdrawBank()
    {
        if (bank == null) return;
        bank.Withdraw(moneyPenalty);
    }
}
