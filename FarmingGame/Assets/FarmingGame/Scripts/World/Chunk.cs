using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ChunkWalls))]
public class Chunk : MonoBehaviour
{
    [Header("----- Elements -----")]
    [SerializeField] private GameObject unlockedElements;
    [SerializeField] private GameObject lockedElements;
    [SerializeField] private TMP_Text priceText;
    private ChunkWalls _chunkWalls;


    [Header("------ Setting ------")]
    [SerializeField] private int initialPrice;
    private int _currentPrice;
    private bool unlocked;

    [Header("------ Actions ------")]
    public static Action OnUnlocked;
    public static Action OnPriceChanged;


    private void Awake()
    {
        _chunkWalls = GetComponent<ChunkWalls>();
    }
    
    public void Initialize(int loadedPrice)
    {
        _currentPrice = loadedPrice;
        priceText.text = _currentPrice.ToString();

        if (_currentPrice <= 0)
        {
            Unlock(false);
        }
    }

    public void TryUnlock()
    {
        if (CashManager.Instance.Coins <= 0)
        {
            Debug.LogWarning("No more many !");
            return;
        }
        _currentPrice--;
        CashManager.Instance.UseCoins(1);
        OnPriceChanged?.Invoke();
        priceText.text = _currentPrice.ToString();
        if (_currentPrice <= 0)
        {
            Unlock();
        }

    }

    private void Unlock(bool triggerAction = true)
    {
        unlockedElements.SetActive(true);
        lockedElements.SetActive(false);

        unlocked = true;

        if (triggerAction)
            OnUnlocked?.Invoke();
    }

    public void UpdateWalls(int configuration)
    {
        _chunkWalls.Configure(configuration);
    }

    public void DisplayLockedElements()
    {
        lockedElements.SetActive(true);
    }

    public bool IsUnlocked()
    {
        return unlocked;
    }

    public int GetInitialPrice()
    {
        return initialPrice;
    }

    public int GetCurrentPrice()
    {
        return _currentPrice;
    }
}
