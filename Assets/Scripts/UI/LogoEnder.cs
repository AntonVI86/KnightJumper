using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class LogoEnder : MonoBehaviour
{
    [SerializeField] private float _timeToLoading;
    [SerializeField] private VideoPlayer _videoPlayer;

    private void Start()
    {
        //OnApplicationFocus(true);
        StartCoroutine(LoadingGame());
        _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Logo.mp4");

        _videoPlayer.Play();
    }

    private IEnumerator LoadingGame()
    {
        yield return new WaitForSeconds(_timeToLoading);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

