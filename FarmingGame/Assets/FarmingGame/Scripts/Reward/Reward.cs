using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Reward
{

    private float _rewardCoin;
    private float _maxRewardCoin =100f;
    private float _minRewardCoin = 500f;

    private float _rewardCoinMultiplier;
    private float _maxRewardCoinMultiplier =1;
    private float _minRewardCoinMultiplier = 1.5f;

    private TMP_Text _rewardAmountText;

    public Reward(TMP_Text rewardAmountText)
    {
        _rewardAmountText = rewardAmountText;
    }

     public float GetRandomRewardCoin()
    {
        _rewardCoin = Random.Range(_maxRewardCoin, _minRewardCoin);
        return _rewardCoin;
    }

    public float GetRandomRewardMultiplier()
    {
        _rewardCoinMultiplier = Random.Range(_maxRewardCoinMultiplier, _minRewardCoinMultiplier);
        return _rewardCoinMultiplier;
    }

    public void GetCoin()
    {
        float coin = GetRandomRewardCoin();
        float multiplier = GetRandomRewardMultiplier();

        _rewardCoin = (_rewardCoin * multiplier)* PlayerPrefs.GetInt("CurrentLevel");

        
        _rewardAmountText.text = Mathf.Round(_rewardCoin).ToString();
    }

    public void UpdateRewardCoinText()
    {
        GetCoin();
        
    }

    public void AddRewardCoin()
    {
        int amount = (int)_rewardCoin;
        CashManager.Instance.AddCoins(amount);
    }

}

