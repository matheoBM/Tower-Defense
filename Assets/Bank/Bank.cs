using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startBalance = 150;
    [SerializeField] int currentBalance;
    [SerializeField] TMP_Text text;//Or TextMeshProUGUI;
    public int CurentBalance { get { return currentBalance; } }

    void Awake()
    {
        currentBalance = startBalance;    
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateTextOnUI();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);

        //Reload when money is over
        if(currentBalance < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        UpdateTextOnUI();
    }

    void UpdateTextOnUI()
    {
        text.text = $"Coins: {currentBalance}";
    }
}
