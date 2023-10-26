using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using Lean.Localization;
using System.Collections.Generic;

public class AbilityView : MonoBehaviour
{
    [SerializeField] private KnightAbilities _abilities;
    [SerializeField] private Ability _ability;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private TMP_Text _warning;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _abilityButton;
    [SerializeField] private GameObject _notchTemplate;
    [SerializeField] private Transform _progressBar;

    [SerializeField] private List<GameObject> _notchList = new List<GameObject>();

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _abilityButton.onClick.AddListener(OnUpgradeAbility);
    }

    private void OnDisable()
    {
        _abilityButton.onClick.RemoveListener(OnUpgradeAbility);
    }

    private void Awake()
    {
        for (int i = 0; i < _ability.MaxLevel; i++)
        {
            GameObject newNotch = Instantiate(_notchTemplate, _progressBar);
            _notchList.Add(newNotch);
            newNotch.SetActive(false);
        }

        _ability.GetPrice();
        Show();
        
        ShowCost();
        _icon.sprite = _ability.Icon;
    }

    private void OnUpgradeAbility()
    {
        float abilityValue = PlayerPrefs.GetFloat(_ability.AbilityName);

        if (_abilities.Money >= _ability.Cost && PlayerPrefs.GetInt(_ability.AbilityName + "Level") < _ability.MaxLevel)
        {          
            AddNotch();
            PlayerPrefs.SetInt("Money", _abilities.Money);

            abilityValue += _ability.AddValue;

            PlayerPrefs.SetFloat(_ability.AbilityName, abilityValue);
        }
        else
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ShowWarningText());
        }
    }

    private void AddNotch()
    {    
        if(PlayerPrefs.GetInt(_ability.AbilityName + "Level") < _ability.MaxLevel)
        {
            _abilities.AddMoney(-_ability.Cost);
            _ability.GrowPrice();
        }

        Show();
        ShowCost();
    }

    private void Show()
    {       
        for (int i = 0; i < PlayerPrefs.GetInt(_ability.AbilityName + "Level"); i++)
        {
            _notchList[i].SetActive(true);
        }
    }

    private void ShowCost()
    {
        _cost.text = _ability.Cost.ToString();

        if(PlayerPrefs.GetInt(_ability.AbilityName + "Level") == _ability.MaxLevel)
        {
            _cost.text = "MAX";
            _abilityButton.interactable = false;
        }
    }

    private IEnumerator ShowWarningText()
    {
        _warning.gameObject.SetActive(true);
        _warning.color = new Color(1,1,1,1);
        yield return new WaitForSecondsRealtime(1.9f);
        _warning.gameObject.SetActive(false);
    }
}
