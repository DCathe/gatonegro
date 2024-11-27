using UnityEngine;
using UnityEngine.Events;

public class Grabber : MonoBehaviour
{ 
    public UnityEvent GrabbedACoin;

    private void OnTriggerEnter(Collider other)
    {
        CoinMovement coinMovement = other.GetComponentInParent<CoinMovement>();
        if (coinMovement == null)
        {
            return;
        }
        TimeToLive timeToLive = coinMovement.GetComponent<TimeToLive>();
        timeToLive.enabled = false;

        coinMovement.enabled = false;        
        coinMovement.transform.SetParent(transform, false);
        coinMovement.transform.localPosition = Vector3.zero;
        GrabbedACoin.Invoke();
    }
}
