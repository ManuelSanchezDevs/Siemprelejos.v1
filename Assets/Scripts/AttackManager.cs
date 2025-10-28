
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int idAttackArrow;

    private void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
    }

    private void Start()
    {
        idAttackArrow = Animator.StringToHash("AttackArrow");
    }


    public void PerformAttack()
    {

        animator.SetTrigger(idAttackArrow);

    }

    public void StopAttack()
    {
        animator.ResetTrigger(idAttackArrow);
    }
}
