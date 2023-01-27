using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    
    private int _currentScore;

    public void ResetScore()
    {
        _currentScore = 0;
    }

    public void AddScore(int score)
    {
        _currentScore += score;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log(_currentScore);
    }
    public int GetCurrentScore()
    {
        return _currentScore;
    }
}
