using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textHolder;

    private int _score = 0;

    private void Start()
    {
        if (_textHolder == null)
        {
            throw new UnityException("There is no text assigned for score");
        }
    }

    public void AddOnePoint()
    {
        _score++;
        _textHolder.text = _score.ToString("D3");
    }
}
