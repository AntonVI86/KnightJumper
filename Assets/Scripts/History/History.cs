using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class History : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    [SerializeField] private Sprite[] _pictures;
    [SerializeField] private string[] _textHistory;

    private void Start()
    {
        StartCoroutine(Demonstration());
    }

    private IEnumerator Demonstration()
    {
        var delay = new WaitForSeconds(10f);

        for (int i = 0; i < _pictures.Length; i++)
        {
            _image.sprite = _pictures[i];
            _text.text = Lean.Localization.LeanLocalization.GetTranslationText(_textHistory[i]);
            yield return delay;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
