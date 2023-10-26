using Agava.YandexGames;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LeaderboardView : MonoBehaviour
{
    private const string Anonymous = "Anonymous";
    private const string LeaderboardName = "leader";

    [SerializeField] private Transform _container;
    [SerializeField] private KnightAbilities _knightAbility;
    [SerializeField] private GameObject _stats;
    [SerializeField] private GameObject _leaderboardTable;
    [SerializeField] private GameObject _authorizationPanel;
    [SerializeField] private Button _authButton;
    [SerializeField] private TMP_Text _mainLable;
    [SerializeField] private PlayerData _playerDataTamplate;
    [SerializeField] private AudioClip _pressButtonSnd;

    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _statsButton;

    private List<PlayerData> _playerDatas = new List<PlayerData>();

    private void OnEnable()
    {
        _leaderboardButton.onClick.AddListener(OnClickLeaderboardButton);
        _statsButton.onClick.AddListener(OnClickStatsButton);
        _authButton.onClick.AddListener(OnAuthorize);
    }

    private void OnDisable()
    {
        _leaderboardButton.onClick.RemoveListener(OnClickLeaderboardButton);
        _statsButton.onClick.RemoveListener(OnClickStatsButton);
        _authButton.onClick.RemoveListener(OnAuthorize);
    }

    private void OnClickLeaderboardButton()
    {
        _stats.SetActive(false);
        _mainLable.text = Lean.Localization.LeanLocalization.GetTranslationText("Leaderboard");
        _leaderboardButton.gameObject.SetActive(false);
        _statsButton.gameObject.SetActive(true);
        _leaderboardTable.gameObject.SetActive(true);
        SoundPlayer.Instance.PlaySound(_pressButtonSnd);

        LoadLeaderboard();
    }

    private void OnClickStatsButton()
    {
        _stats.SetActive(true);
        _mainLable.text = Lean.Localization.LeanLocalization.GetTranslationText("Character");
        _authorizationPanel.SetActive(false);
        _leaderboardButton.gameObject.SetActive(true);
        _leaderboardTable.gameObject.SetActive(false);
        _statsButton.gameObject.SetActive(false);
        SoundPlayer.Instance.PlaySound(_pressButtonSnd);
    }

    public void LoadLeaderboard()
    {
#if YANDEX_GAMES
        if (YandexGamesSdk.IsInitialized == false)
            return;

        Autorize();
        //SetScore();
        ClearLeaderboard();

        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = Anonymous;

                if(_container.childCount < 6)
                {
                    PlayerData playerData = Instantiate(_playerDataTamplate, _container);
                    playerData.Initialize(name, entry.rank, entry.score);
                    _playerDatas.Add(playerData);
                }
                
            }
        });
#endif

#if VK_GAMES
        Agava.VKGames.Leaderboard.ShowLeaderboard(_score.Score);
#endif
    }

    private void Autorize()
    {
        if (PlayerAccount.IsAuthorized == false)
        {
            _leaderboardTable.gameObject.SetActive(false);
            _authorizationPanel.SetActive(true);

            //for (int i = 1; i < _container.childCount; i++)
            //{
            //    Destroy(transform.GetChild(i).gameObject);
            //}

            //PlayerData playerData = Instantiate(_playerDataTamplate, _container);
            //playerData.ShowScoreAnonimous(Anonymous, _knightAbility.Score);
        }
        //PlayerAccount.Authorize();
    }

    private void OnAuthorize()
    {
        PlayerAccount.Authorize();
    }

    public void SetScore()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        Leaderboard.SetScore(LeaderboardName, _knightAbility.Score);
    }

    private void ClearLeaderboard()
    {
        foreach (PlayerData entry in _playerDatas)
            Destroy(entry.gameObject);

        _playerDatas = new List<PlayerData>();
    }
}
