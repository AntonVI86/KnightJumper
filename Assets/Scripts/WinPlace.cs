using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class WinPlace : MonoBehaviour
{
    [SerializeField] private KnightAbilities _abilities;
    [SerializeField] private Transform _winPosition;
    [SerializeField] private Camera _win;
    [SerializeField] private Camera _main;
    [SerializeField] private Image _winWhiteScreen;
    [SerializeField] private AudioClip _winMusic;

    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        _abilities.WinScoreReached += OnWin;
    }

    private void OnDisable()
    {
        _abilities.WinScoreReached -= OnWin;
    }

    private void OnWin()
    {
        _abilities.gameObject.transform.SetParent(null);
        _abilities.WinScoreReached -= OnWin;

        if (_audio.isPlaying == false)
            _audio.Play();

        StopAllCoroutines();

        _winWhiteScreen.DOFade(1, 1f).OnComplete(() => { TeleportPlayer(); });       
    }

    private void TeleportPlayer()
    {
        _main.enabled = false;
        _win.enabled = true;
        _abilities.GetComponent<Transform>().position = _winPosition.position;
        _winWhiteScreen.DOFade(0, 0.1f);

        _audio.PlayOneShot(_winMusic);
    }
}
