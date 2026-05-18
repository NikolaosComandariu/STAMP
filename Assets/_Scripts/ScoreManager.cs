using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Player scores.
    private int p1Score, p2Score = 0;

    public static event Action<int, int> SendPlayerScores;

    /// <summary>
    /// Subscribe to events.
    /// </summary>
    private void OnEnable()
    {
        ObjectSpawner.IncrementP1Score += P1Score;
        ObjectSpawner.IncrementP2Score += P2Score;
        GameManager.onGameOver += SendScores;
        GameManager.onNextRound += SendScores;
    }

    /// <summary>
    /// Unsubscribe from events.
    /// </summary>
    private void OnDisable()
    {
        ObjectSpawner.IncrementP1Score -= P1Score;
        ObjectSpawner.IncrementP2Score -= P2Score;
        GameManager.onGameOver -= SendScores;
        GameManager.onNextRound -= SendScores;
    }

    /// <summary>
    /// Update player 1 score.
    /// </summary>
    /// <param name="amount"></param>
    public void P1Score()
    {
        p1Score++;
    }

    /// <summary>
    /// Update player 2 score.
    /// </summary>
    /// <param name="amount"></param>
    private void P2Score()
    {
        p2Score++;
    }

    private void SendScores()
    {
        SendPlayerScores?.Invoke(p1Score, p2Score);
    }
}