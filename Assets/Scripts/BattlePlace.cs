using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class BattlePlace : MonoBehaviour
{
    [SerializeField] private KnightAbilities _abilities;
    [SerializeField] private Image _winWhiteScreen;

    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        _abilities.KeyIsCompleted += OnWin;
    }

    private void OnDisable()
    {
        _abilities.KeyIsCompleted -= OnWin;
    }

    private void OnWin()
    {
        _abilities.KeyIsCompleted -= OnWin;

        if (_audio.isPlaying == false)
            _audio.Play();

        StopAllCoroutines();

        _winWhiteScreen.DOFade(1, 1f).OnComplete(() => { SceneManager.LoadScene("FinalBattle"); });       
    }
}
