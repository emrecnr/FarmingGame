using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager Instance { get; private set; }
    [Header("Settings")]
    private int _coins; 

    #region Properties
    public int Coins => _coins;
    #endregion
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(this);
    }
    private void Start()
    {
        LoadData();
    }
    public void UseCoins(int amount)
    {
        AddCoins(-amount);
    }


    public void AddCoins(int amount)
    {
        _coins += amount;
        UpdateCoinContainers();

        SaveData();
    }

    private void UpdateCoinContainers()
    {
        GameObject[] coinContainers = GameObject.FindGameObjectsWithTag("CoinAmount");
        foreach (GameObject coinContainer in coinContainers)
        {
            coinContainer.GetComponent<TMP_Text>().text = _coins.ToString();
        }
    }

    private void LoadData()
    {
        if(PlayerPrefs.HasKey("Coins"))
        {
            _coins = PlayerPrefs.GetInt("Coins");
            UpdateCoinContainers();

            return;
        }
        _coins = 50;
        UpdateCoinContainers();

    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("Coins", _coins);
    }
}
