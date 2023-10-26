using UnityEngine;
using Agava.YandexGames;
using UnityEngine.UI;

public class Ads : MonoBehaviour
{
    [SerializeField] private KnightAbilities _knightAbilities;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private SoundSwitcher _soundSwitcher;
    [SerializeField] private AudioClip _addMoneySnd;

    private void OnEnable()
    {
        _rewardButton.interactable = true;
        _rewardButton.onClick.AddListener(ShowRewardVideo);
    }

    private void OnDisable()
    {
        _rewardButton.onClick.RemoveListener(ShowRewardVideo);
    }

    private void Start()
    {
        if (YandexGamesSdk.IsInitialized == false)
            return;
        
        InterstitialAd.Show(OnAdOpen, OnAdClose);       
    }

    public void ShowRewardVideo()
    {
        VideoAd.Show(OnRewardAdOpen, GetReward, OnRewardAdClose);
    }

    private void GetReward()
    {
        int tempValue = 0;

        foreach (var ability in _knightAbilities._abilities)
        {
            tempValue += ability.Cost;
        }

        tempValue = tempValue / _knightAbilities._abilities.Count - 1;

        _knightAbilities.AddMoney(tempValue);
    }

    public void OnRewardAdOpen()
    {
        _knightAbilities.TakeOffBox();
        UnityEngine.PlayerPrefs.SetFloat("BoxCount", _knightAbilities.Capacity);

        _rewardButton.interactable = false;

        _soundSwitcher.TurnAudio(0f);
        Time.timeScale = 0f;
    }
    public void OnAdOpen()
    {
        _soundSwitcher.TurnAudio(0f);
        Time.timeScale = 0f;
    }

    public void OnRewardAdClose()
    {
        _soundSwitcher.OnMute();
        Time.timeScale = 1f;

        if (_knightAbilities.Capacity != 0)
        {
            _rewardButton.interactable = true;
        }

        SoundPlayer.Instance.PlaySound(_addMoneySnd);
    }
    public void OnAdClose(bool isClosed)
    {
        _soundSwitcher.OnMute();
        Time.timeScale = 1f;
    }
}
