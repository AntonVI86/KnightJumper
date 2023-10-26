using TMPro;
using UnityEngine;

public class PartsKeyView : MonoBehaviour
{
    [SerializeField] private KnightAbilities _abilities;

    private TMP_Text _partsKey;

    private void Awake()
    {
        _partsKey = GetComponent<TMP_Text>();    
    }

    private void OnEnable()
    {
        _abilities.PartsKeyValueChanged += OnPartsKeyValueChanged;
    }

    private void OnDisable()
    {
        _abilities.PartsKeyValueChanged -= OnPartsKeyValueChanged;
    }

    private void OnPartsKeyValueChanged(int value)
    {
        _partsKey.text = $"{value}/5";
    }
}
