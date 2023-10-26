using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextOutputer : MonoBehaviour
{
    [SerializeField] private AudioClip _printSFX;

    private float _delay = 0.05f;

    private Coroutine _coroutine;

    public void StartOutput(string s, TMP_Text text)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Output(s, text));
    }

    public void StopOutput()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator Output(string s, TMP_Text text)
    {
        var delay = new WaitForSeconds(_delay);
        text.text = "";

        for (int i = 0; i < s.Length; i++)
        {
            text.text = $"{text.text}{s[i]}";
            yield return delay;
        }
    }
}
