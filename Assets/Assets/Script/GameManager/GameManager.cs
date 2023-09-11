using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _healthText;
    [SerializeField]
    private GameObject _losePanel;
    [SerializeField]
    private GameObject _winPanel;
    [SerializeField]
    private Button _restartWinPanel;
    [SerializeField]
    private Button _restartLosePanel;

    private int _health = 3;

    private void Start()
    {
        _restartWinPanel.onClick.RemoveAllListeners();
        _restartWinPanel.onClick.AddListener(Restart);
        _restartLosePanel.onClick.RemoveAllListeners();
        _restartLosePanel.onClick.AddListener(Restart);
        UpdateUI();
    }

    public void Win()
    {
        _winPanel.SetActive(true);
    }

    public void Lose()
    {
        _losePanel.SetActive(true);
    }

    public void SubtractHealth()
    {
        _health--;
        UpdateUI();
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            Lose();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void UpdateUI()
    {
        _healthText.text = $"Health: {_health}";
    }
}
