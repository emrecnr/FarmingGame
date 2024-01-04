using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    [Header("----- Elements -----")]
    [SerializeField] private Button _rewardButton;
    [SerializeField] private GameObject _rewardPanel;
    [SerializeField] private TMP_Text _rewardAmountText;
    private Reward _reward;

    [Header("----- Settings -----")]
    private bool _rewardButtonActive = false;

    private float _rewardTimer =5f; 
    private float _minRewardTime = 300f;
    private float _maxRewardTime = 600f;


    private void Awake()
    {
        _reward = new Reward(_rewardAmountText);
    }

    private void Start()
    {
        _rewardButton.gameObject.SetActive(false);
        StartCoroutine(RewardTimer());
    }
    
    private IEnumerator RewardTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(_rewardTimer);
            ActivateRewardButton();
            break;
        }
    }

    public void ClaimReward()
    {
        _reward.AddRewardCoin();
        _rewardPanel.gameObject.SetActive(false);
    }
    
    private void ActivateRewardButton()
    {
        _rewardButtonActive = true;
        _rewardButton.gameObject.SetActive(true);
        LeanTween.scale(_rewardButton.gameObject, Vector3.one*175f, .75f)
        .setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
                {

                    // Animasyon tamamlandığında paneli aç
                    LeanTween.delayedCall(.1f, () =>
                    {
                        OpenRewardPanel();
                    });
                }); ;
        Debug.Log("Reward button activated!");

    }

    private void OpenRewardPanel()
    {
        _reward.GetCoin();

        _rewardButton.gameObject.SetActive(false);
        _rewardPanel.gameObject.SetActive(true);
    }
}
