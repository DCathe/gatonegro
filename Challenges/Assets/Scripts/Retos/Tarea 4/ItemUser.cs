using System.Linq;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class ItemUser : MonoBehaviour
{
    [SerializeField]
    private Transform _itemCheckerTransform;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private LayerMask _itemLayers;

#if ENABLE_INPUT_SYSTEM
    public void OnUse(InputValue inputValue)
    {
        if (inputValue.isPressed) 
        {
            Collider[] itemColliders = Physics.OverlapSphere(_itemCheckerTransform.position, _radius, _itemLayers);
            UsableItem[] usableItems = itemColliders.Select(x => x.GetComponent<UsableItem>()).ToArray();
            foreach (UsableItem usableItem in usableItems)
            {
                usableItem.UseItem();
            }
        }
    }
#endif

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_itemCheckerTransform.position, _radius);
    }
}
