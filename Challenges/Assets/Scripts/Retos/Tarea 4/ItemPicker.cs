using UnityEngine;
using UnityEngine.Events;

public class ItemPicker : MonoBehaviour
{
    public UnityEvent<string> OnItemPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        PickableItem pickable = other.GetComponentInParent<PickableItem>();
        if (pickable != null)
        {
            string tag = pickable.gameObject.tag;
            OnItemPickedUp.Invoke(tag);
            pickable.OnPickUp();
        }
    }
}
