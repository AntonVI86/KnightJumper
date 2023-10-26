using System.Collections;
using Agava.YandexGames;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class YandexInitializer : MonoBehaviour
{
    private const string LeaderboardName = "leader";

    public event UnityAction PlayerAuthorized;

    private IEnumerator Start()
    {
#if YANDEX_GAMES
        yield return YandexGamesSdk.Initialize(() => PlayerAccount.RequestPersonalProfileDataPermission());

        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result != null)
                PlayerAuthorized?.Invoke();
        });

        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang);
#endif

#if VK_GAMES
        yield return VKGamesSdk.Initialize();
#endif
    }
}
