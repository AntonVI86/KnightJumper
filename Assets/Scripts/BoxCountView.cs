using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class BoxCountView : MonoBehaviour
{
    [SerializeField] private KnightAbilities _abilities;
    [SerializeField] private Button _rewardButton;

    private TMP_Text _view;

    private void Awake()
    {
        _view = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _abilities.BoxTaked += OnCapacityValueChanged;
    }
    private void OnDisable()
    {
        _abilities.BoxTaked -= OnCapacityValueChanged;
    }

    private void Start()
    {
        OnCapacityValueChanged(PlayerPrefs.GetFloat("BoxCount"));
    }

    private void OnCapacityValueChanged(float value)
    {
        _view.text = $"{value}/{_abilities.MaxCapacity}";

        if (PlayerPrefs.GetFloat("BoxCount") <= 0)
            _rewardButton.interactable = false;
    }

    public void OnClick()
    {
        _view.text = $"{PlayerPrefs.GetFloat("BoxCount")}/{_abilities.MaxCapacity}";
    }
}
