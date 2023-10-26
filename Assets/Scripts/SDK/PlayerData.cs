using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _rank;
    [SerializeField] private TMP_Text _score;

    public void Initialize(string name, int rank, int score)
    {
        _name.text = name;
        _rank.text = rank.ToString();
        _score.text = score.ToString();
    }

    public void ShowScoreAnonimous(string name, int score)
    {
        _name.text = name;
        _score.text = score.ToString();
    }
}
