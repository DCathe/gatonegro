using UnityEngine;
using UnityEngine.Events;

public class UsableItem : MonoBehaviour
{
    public UnityEvent OnItemUsed;

    public void UseItem()
    {
        OnItemUsed.Invoke();
    }
}
