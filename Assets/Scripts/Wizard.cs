using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : MonoBehaviour
{
    [SerializeField] private KnightAbilities _abilities;
    [SerializeField] private TMP_Text _phrase;
    [SerializeField] private GameObject _dialogCloud;
    [SerializeField] private Button _closeButton;

    [SerializeField] private List<LeanSource> _firstPhrases = new List<LeanSource>();
    [SerializeField] private List<LeanSource> _secondPhrases = new List<LeanSource>();
    [SerializeField] private List<LeanSource> _thirdPhrases = new List<LeanSource>();
    [SerializeField] private List<LeanSource> _fourthPhrases = new List<LeanSource>();
    [SerializeField] private List<LeanSource> _fifthPhrases = new List<LeanSource>();

    private List<List<LeanSource>> _phrases = new List<List<LeanSource>>();
    [SerializeField] private List<LeanSource> _currentPhrases = new List<LeanSource>();

    private TextOutputer _outputer;
    private Coroutine _coroutine;
    private PlayerInput _input;
    private int index = 0;

    private void Awake()
    {
        _input = new PlayerInput();
        _outputer = GetComponent<TextOutputer>();
    }
    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Start()
    {
        if (_abilities.PartsKey >= 5)
        {
            gameObject.SetActive(false);
        }

        _closeButton.gameObject.SetActive(false);

        _phrases.Add(_firstPhrases);
        _phrases.Add(_secondPhrases);
        _phrases.Add(_thirdPhrases);
        _phrases.Add(_fourthPhrases);
        _phrases.Add(_fifthPhrases);

        if(_abilities.PartsKey <= 4)
            _currentPhrases = _phrases[_abilities.PartsKey];

        _dialogCloud.SetActive(false);
    }
    public void WritePharse()
    {
        _dialogCloud.SetActive(true);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartMonolog());
    }

    private IEnumerator StartMonolog()
    {       
        bool press = _input.Player.Attack.ReadValue<float>() > 0.1f;
        string s = "";

        while (index < _currentPhrases.Count)
        {
            s = LeanLocalization.GetTranslationText(_currentPhrases[index].name);
            _outputer.StartOutput(s, _phrase);
            yield return new WaitUntil(() => _input.Player.Attack.ReadValue<float>() > 0.1f);
            yield return new WaitForSeconds(0.2f);
            _outputer.StopOutput();
            _phrase.text = s;
            yield return new WaitUntil(() => _input.Player.Attack.ReadValue<float>() > 0.1f);
            yield return new WaitForSeconds(0.2f);
            index++;
        }

        _abilities.AddPartKey();
        _abilities.AddMaxScore();
        _closeButton.gameObject.SetActive(true);

    }
}
