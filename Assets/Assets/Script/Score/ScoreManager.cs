using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    private int _score;

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int value)
    {
        _score += value;
        UpdateUI();
    }

    public void UpdateUI()
    {
        _scoreText.text = $"Score: {_score}";
    }
}
