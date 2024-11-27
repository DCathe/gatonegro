using Challenges.Constants;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerStatusManager : MonoBehaviour
{
    private Animator _animator;

    private int _isGrowingHashId;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _isGrowingHashId = Animator.StringToHash("IsGrowing");
    }

    public void OnItemPickedUp(string itemTag)
    {
        if (itemTag == Tags.Mushroom)
        {
            _animator.SetBool(_isGrowingHashId, true);
        }
    }
}
