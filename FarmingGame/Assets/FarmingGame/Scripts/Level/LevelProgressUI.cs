using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressUI : MonoBehaviour
{
    [Header("--- Elements ---")]
    [SerializeField] private Image uiFillImage; 
    [SerializeField] private TMP_Text uiStartText;
    [SerializeField] private TMP_Text uiEndText;


    public void UpdateFill(float fillAmount)
    {
        LeanTween.value(uiFillImage.gameObject, uiFillImage.fillAmount, fillAmount, 1)
        .setOnUpdate((float val) =>
        {
            uiFillImage.fillAmount = val;
        });
    }
    public void UpdateText(int startText,int endText)
    {
        uiStartText.text = startText.ToString();
        uiEndText.text = endText.ToString();

    }
}
