using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private KnightAbilities _knight;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _addingScore;

    private void Start()
    {
        _addingScore.color = new Color(0, 1, 0, 0);
    }
    private void OnEnable()
    {
        _knight.ScoreValueChanged += OnScoreValueChanged;
    }

    private void OnDisable()
    {
        _knight.ScoreValueChanged -= OnScoreValueChanged;
    }

    private void OnScoreValueChanged(int score)
    {
        _score.text = score.ToString();
        _addingScore.color = new Color(0, 1, 0, 1);
        _addingScore.DOFade(0, 1f);
    }
}
