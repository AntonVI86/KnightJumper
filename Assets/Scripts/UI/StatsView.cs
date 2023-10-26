using UnityEngine;
using TMPro;

public class StatsView : MonoBehaviour
{
    [SerializeField] private KnightAbilities _abilities;
    [SerializeField] private TMP_Text _speed;
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _jumpForce;

    private void Start()
    {
        Show();
    }
    public void Show()
    {
        _speed.text = PlayerPrefs.GetFloat("Speed").ToString();
        _health.text = PlayerPrefs.GetFloat("Health").ToString();
        _jumpForce.text = PlayerPrefs.GetFloat("JumpForce").ToString();
    }
}
