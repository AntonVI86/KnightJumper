using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _continueButton;

    [SerializeField] private Image _loadingViewImage;

    private KnightAbilities _abilities;

    private void Awake()
    {
        _abilities = GetComponent<KnightAbilities>();
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnLoadNewGame);
        _continueButton.onClick.AddListener(OnContinueButton);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnLoadNewGame);
        _continueButton.onClick.RemoveListener(OnContinueButton);
    }

    private void Start()
    {
        _continueButton.interactable = false;

        if (PlayerPrefs.HasKey("Money"))
        {
            _continueButton.interactable = true;
        }       
    }

    public void OnLoadNewGame()
    {
        StartCoroutine(AsyncLoad());
    }

    public void OnContinueButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator AsyncLoad()
    {
        _startButton.interactable = false;
        _continueButton.interactable = false;
        _abilities.ResetToDefault();
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while (operation.isDone == false)
        {
            float progress = operation.progress/0.9f;
            _loadingViewImage.fillAmount = progress;

            yield return null;
        }
    }
}
