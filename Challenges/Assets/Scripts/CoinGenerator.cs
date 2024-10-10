using System.Collections;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField]
    private float _minTime = 1.0f;

    [SerializeField]
    private float _maxTime = 5.0f;

    [SerializeField]
    private GameObject _coinPrefab;

    [SerializeField]
    private float _startTimeToInstantiate = 1.0f;

    private void Start()
    {
        if (_coinPrefab == null)
        {
            throw new UnityException("No prefab have been selected");
        }
        StartCoroutine(CreateCoinsInRandomPeriods());
    }

    private IEnumerator CreateCoinsInRandomPeriods()
    {
        yield return new WaitForSeconds(_startTimeToInstantiate);

        while (true)
        {
            Instantiate(_coinPrefab, transform.position, transform.rotation, transform);
            float waitInSecondsForNextInstance = Mathf.Max(_minTime, Random.value * _maxTime);
            yield return new WaitForSeconds(waitInSecondsForNextInstance);
        }
    }
}
