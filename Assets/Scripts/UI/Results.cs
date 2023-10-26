using TMPro;
using UnityEngine;

public class Results : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentScore;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private KnightAbilities _abilities;

    private int _hightScore;

    private void Start()
    {
        if(PlayerPrefs.HasKey("HighScore"))
            _hightScore = PlayerPrefs.GetInt("HighScore");

        if(_abilities.Score > _hightScore)
        {
            _hightScore = _abilities.Score;
        }       

        _currentScore.text = _abilities.Score.ToString();
        _highScoreText.text = _hightScore.ToString();

        PlayerPrefs.SetInt("HighScore", _hightScore);
    }
}
