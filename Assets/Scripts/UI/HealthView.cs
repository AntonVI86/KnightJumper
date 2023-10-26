using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private KnightAbilities _abilities;
    [SerializeField] private GameObject _heartPrefab;

    private List<GameObject> _hearts = new List<GameObject>();

    private void Awake()
    {
        CreateHearts(PlayerPrefs.GetFloat("Health"));
    }

    private void OnEnable()
    {
        _abilities.HealthValueChanged += OnHealthValueChanged;
    }

    private void OnDisable()
    {
        _abilities.HealthValueChanged -= OnHealthValueChanged;
    }

    private void OnHealthValueChanged(float healthValue)
    {
        foreach (var item in _hearts)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < healthValue; i++)
        {
            _hearts[i].gameObject.SetActive(true);
        }
    }

    private void CreateHearts(float healthValue)
    {
        for (int i = 0; i < healthValue; i++)
        {
            GameObject heartSpawned = Instantiate(_heartPrefab, transform);

            _hearts.Add(heartSpawned);
        }
    }
}
