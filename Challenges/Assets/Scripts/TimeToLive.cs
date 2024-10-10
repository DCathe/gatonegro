using System.Collections;
using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    [SerializeField]
    private float _timeToLive = 10f;

    public void StopKill()
    {
        StopCoroutine(KillAfterTimeToLive());
    }

    private void Start()
    {
        StartCoroutine(KillAfterTimeToLive());
    }

    private IEnumerator KillAfterTimeToLive()
    {
        yield return new WaitForSeconds(_timeToLive);
        Destroy(gameObject);
    }
}
