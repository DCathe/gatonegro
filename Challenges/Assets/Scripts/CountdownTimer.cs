using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    private float _timeAmount = 60f;

    [SerializeField]
    private TMP_Text _textHolder;

    public UnityEvent TimeHasRunOut;

    private float _currentTimeAmount = 0;

    private void Start()
    {
        _currentTimeAmount = _timeAmount;
        StartCoroutine(StartCountdown());
    }
    
    private IEnumerator StartCountdown()
    {
        while (true)
        {
            _currentTimeAmount -= Time.deltaTime;
            if (_currentTimeAmount < 0)
            {
                TimeHasRunOut.Invoke();
                break;
            }
            string timerText = FormatToTimer(_currentTimeAmount);
            _textHolder.text = timerText;
            yield return null;
        }        
    }

    private string FormatToTimer(float timeToFormat)
    {
        int minutes = Mathf.FloorToInt(timeToFormat / 60);
        int remaininSeconds = Mathf.FloorToInt(timeToFormat % 60);
        return $"{minutes:D2}:{remaininSeconds:D2}";
    }
}
