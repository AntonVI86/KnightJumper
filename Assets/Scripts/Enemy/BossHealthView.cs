using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthView : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private Slider _healthBar;
    private Coroutine _coroutine;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _enemy.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _enemy.Damaged -= OnDamaged;
    }

    private void Start()
    {
        _healthBar.maxValue = _enemy.Health;
        _healthBar.value = _healthBar.maxValue;       
    }

    private void OnDamaged()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        StartCoroutine(ChangeHealthValue());       
    }

    private IEnumerator ChangeHealthValue()
    {
        while(_healthBar.value != _enemy.Health)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, _enemy.Health, Time.deltaTime);
            yield return null;
        }

        if (_healthBar.value <= 0)
            gameObject.SetActive(false);
    }
}
