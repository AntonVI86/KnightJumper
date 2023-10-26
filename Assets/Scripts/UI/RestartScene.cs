using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartScene : MonoBehaviour
{
    private Button _restart;

    private void Awake()
    {
        _restart = GetComponent<Button>();    
    }

    private void OnEnable()
    {
        _restart.onClick.AddListener(OnclickRestartButton);
    }

    private void OnDisable()
    {
        _restart.onClick.RemoveListener(OnclickRestartButton);
    }

    private void OnclickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
