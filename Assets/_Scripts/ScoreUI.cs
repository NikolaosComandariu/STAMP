using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private bool isPlayer1;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        if (ScoreManager.Instance == null)
        {
            Debug.LogError("ScoreManager.Instance NULL - ScoreUI");
            return;
        }

        if (isPlayer1)
            ScoreManager.Instance.OnPlayer1ScoreChanged += UpdateScore;
        else
            ScoreManager.Instance.OnPlayer2ScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        if (ScoreManager.Instance == null)
            return;

        if (isPlayer1)
            ScoreManager.Instance.OnPlayer1ScoreChanged -= UpdateScore;
        else
            ScoreManager.Instance.OnPlayer2ScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        Debug.Log($"UI updating score: {newScore}");
        scoreText.text = "Score: " + newScore;
    }
}