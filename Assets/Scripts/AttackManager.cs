
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int IdShootX, IdShootUp, IdshootDown;

    private void Start()
    {
        IdShootX = Animator.StringToHash("ShootX");
        IdShootUp = Animator.StringToHash("ShootUp");
        IdshootDown = Animator.StringToHash("ShootDown");
    }

    private void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
    }

    public void PerformAttack(Vector2 direction)
    {
        // Calcula qué animación usar
        float absX = Mathf.Abs(direction.x);
        float absY = Mathf.Abs(direction.y);

        if (absX > absY)
        {
            // Horizontal
            animator.Play(IdShootX, 0, 0);
        }
        else if (direction.y > 0)
        {
            animator.Play("ShootUp", 0, 0);
        }
        else
        {
            animator.Play("ShootDown", 0, 0);
        }
    }

    public void StopAttack()
    {

    }
}
