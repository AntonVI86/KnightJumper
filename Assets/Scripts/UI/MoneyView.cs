using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private KnightAbilities _abilities;

    private void Awake()
    {
        _abilities.MoneyValueChanged += OnMoneyValueChanged;
    }

    private void OnDisable()
    {
        _abilities.MoneyValueChanged -= OnMoneyValueChanged;
    }

    private void OnMoneyValueChanged(int money)
    {
        _money.text = money.ToString();
    }
}
