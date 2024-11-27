using UnityEngine;
using UnityEngine.Events;

public class ScoreGoal : MonoBehaviour
{
    [SerializeField]
    public UnityEvent ScoreGoalReached;

    [SerializeField]
    private int _scoreGoal;

    [SerializeField]
    private int _initialScore = 0;

    private int _currentScore;

    private void Start()
    {
        _currentScore = _initialScore;
    }

    public void AddOnePoint()
    {
        if (++_currentScore >= _scoreGoal)
        {
            ScoreGoalReached.Invoke();
        }
    }
}
