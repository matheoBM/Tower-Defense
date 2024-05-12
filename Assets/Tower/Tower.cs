using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [Tooltip("Time to build each part of the tower")]
    [SerializeField] int timeToBuildPiece = 2;
    Bank bank;

    void Start()
    {
        StartCoroutine(BuildTower());
    }

    public bool PlaceTower(Tower towerPrefab, Vector3 position)
    {
        bank = FindObjectOfType<Bank>();
        if (towerPrefab == null) return false;
        if(bank == null) return false;

        if(bank.CurentBalance >= cost)
        {
            GameObject towerTransform = Instantiate(towerPrefab, position, Quaternion.identity).gameObject;
            foreach (Transform child in towerTransform.transform)
            {
                child.gameObject.SetActive(false);
            }
            bank.Withdraw(cost);
            return true;
        }

        return false;
    }

    IEnumerator BuildTower()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
        }
    }
}
