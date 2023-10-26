using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private KnightAbilities _abilities;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick()
    {
        _abilities.Save();
        Time.timeScale = 1;
        Invoke(nameof(Load), 1f);
    }

    private void Load()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
