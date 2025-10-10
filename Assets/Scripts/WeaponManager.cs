using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorOverrideController iddleOverride;
    private RuntimeAnimatorController animatorBase;
    //[SerializeField] private AnimatorOverrideController archOverride;

    void Start()
    {
        animatorBase = animator.runtimeAnimatorController;
    }

    public void EquipBow()
    {
        animator.runtimeAnimatorController = iddleOverride;
    }

}
