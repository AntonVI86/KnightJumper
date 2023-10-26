using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability", fileName = "newAbility")]
public class Ability : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _label;
    [SerializeField] private string _abilityName;
    [SerializeField] private string _discription;
    [SerializeField] private float _addValue;
    [SerializeField] private int _cost;
    [SerializeField] private int _maxLevel;
    [SerializeField] private int _startCost;

    [SerializeField] private int _level;

    public Sprite Icon => _icon;
    public string Label => _label;
    public string AbilityName => _abilityName;
    public string Discription => _discription;
    public float AddValue => _addValue;
    public int Cost => _cost;

    public int Level => _level;
    public int MaxLevel => _maxLevel;

    public void GrowPrice()
    {
        if (_level < _maxLevel)
        {
            _cost *= 2;
            _level++;
            PlayerPrefs.SetInt(_abilityName + "Level", _level);
            PlayerPrefs.SetInt(_abilityName + "Cost", _cost);
        }
    }

    public void GetPrice()
    {
        _cost = _startCost;

        if(PlayerPrefs.HasKey(_abilityName + "Level"))
            _level = PlayerPrefs.GetInt(_abilityName + "Level");

        if(PlayerPrefs.HasKey(_abilityName + "Cost"))
            _cost = PlayerPrefs.GetInt(_abilityName + "Cost");
    }

    public void ResetLevel()
    {
        _level = 0;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(_abilityName + "Level", _level);
        PlayerPrefs.SetInt(_abilityName + "Cost", _cost);
    }
}
