using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;

    Bank bank;

    public bool PlaceTower(Tower towerPrefab, Vector3 position)
    {
        bank = FindObjectOfType<Bank>();
        if (towerPrefab == null) return false;
        if(bank == null) return false;

        if(bank.CurentBalance >= cost)
        {
            Instantiate(towerPrefab, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }

        return false;
        
    }
}
