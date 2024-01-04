using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("----- Elements -----")]
    [SerializeField] private LevelProgressUI _levelProgressUI;
    #region Level 
    private int _startLevel = 1;
    private int _currentLevel;
    private int _maxLevel = 100;
    #endregion

    #region Exp
    private float _currentExperience;
    private float _expNeededForNextLevel;
    #endregion

    #region Properties
    public int CurrentLevel
    {
        get
        {
            return _currentLevel;
        }
        set
        {
            _currentLevel = value;
        }
    }
    #endregion

    #region Process


    private void Start() 
    {
        LoadLevel();
        _levelProgressUI.UpdateFill(GetFillAmount());
        _levelProgressUI.UpdateText(_currentLevel,_currentLevel +1);

        // Reward script callback
    }
    private void OnEnable() {
        CropTile.OnCropHarvestedExperience += OnCropHarvestedExperienceHandler;
    }

   
    private void OnDisable() 
    {
        CropTile.OnCropHarvestedExperience -= OnCropHarvestedExperienceHandler;
    }


    private void OnCropHarvestedExperienceHandler(float exp)
    {
        AddExperience(exp);
    }


    private void AddExperience(float experience)
    {
        _currentExperience += experience;

        float fillAmount = GetFillAmount();

        _levelProgressUI.UpdateFill(fillAmount);

        if(_currentExperience >= _expNeededForNextLevel) 
                                LevelUp();

        SaveLevel();
    }
    private void LevelUp()
    {
        _currentLevel ++;
        _expNeededForNextLevel *= 1.5f;

        _currentExperience = 0f;
        
        float fillAmount = GetFillAmount();
        _levelProgressUI.UpdateFill(fillAmount);
        _levelProgressUI.UpdateText(_currentLevel,_currentLevel+1);
        SaveLevel();
    }

    private float GetFillAmount()
    {
        return _currentExperience / _expNeededForNextLevel;
    }

    private void SaveLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel",_currentLevel);
        PlayerPrefs.SetFloat("CurrentExp",_currentExperience);
        PlayerPrefs.SetFloat("NextLevelExp",_expNeededForNextLevel);
    }
    private void LoadLevel()
    {
        if(!PlayerPrefs.HasKey("CurrentLevel"))
        {
            _currentLevel = 1;
            _currentExperience = 1f;
            _expNeededForNextLevel = 10f;
        }
        else
        {
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            _currentExperience = PlayerPrefs.GetFloat("CurrentExp");
            _expNeededForNextLevel = PlayerPrefs.GetFloat("NextLevelExp");
        }
    }
    #endregion

}
