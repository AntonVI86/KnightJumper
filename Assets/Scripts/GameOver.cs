using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private KnightAbilities _abilities;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _gameOver;

    private void OnEnable()
    {
        _abilities.Died += OnDied;
    }

    private void OnDisable()
    {
        _abilities.Died -= OnDied;
    }

    private void Start()
    {
        _gameOverPanel.SetActive(false);
    }

    private void OnDied()
    {
        PlayerPrefs.DeleteKey("CurrentHealth");
        PlayerPrefs.SetInt("Money", _abilities.Money);       
        Invoke(nameof(StopTime), 2f);
        _abilities.Died -= OnDied;
        _audio.PlayOneShot(_gameOver);
    }

    private void StopTime()
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
    }
}