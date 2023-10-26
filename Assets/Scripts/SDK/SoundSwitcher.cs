using UnityEngine;
using UnityEngine.UI;

public class SoundSwitcher : MonoBehaviour
{
    public const string Volume = "Volume";

    [SerializeField] private AudioSource[] _audioSources;

    [SerializeField] private Sprite _turnOff;
    [SerializeField] private Sprite _turnOn;

    [SerializeField] private Button _switch;

    private Image _icon;

    [SerializeField] private bool _isTurnOn;

    private void OnEnable()
    {
        _icon = GetComponent<Image>();
        _isTurnOn = PlayerPrefs.GetInt("SoundOn") == 1 ? true : false;
        OnMute();
        _switch.onClick.AddListener(OnClickButton);
    }

    private void OnDisable()
    {
        _switch.onClick.RemoveListener(OnClickButton);
    }

    private void OnClickButton()
    {
        _isTurnOn = !_isTurnOn;
        OnMute();
        PlayerPrefs.SetInt("SoundOn", _isTurnOn ? 1 : 0);
    }

    public void OnMute()
    {
        if(_isTurnOn == false)
        {
            _icon.sprite = _turnOff;
            TurnAudio(0f);
        }
        else if (_isTurnOn)
        {
            _icon.sprite = _turnOn;
            TurnAudio(0.5f);
        }
    }

    public void TurnAudio(float value)
    {
        foreach (var audio in _audioSources)
        {
            audio.volume = value;
        }
    }
}
