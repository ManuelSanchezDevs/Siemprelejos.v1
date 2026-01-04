
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int IdShootX, IdShootUp, IdshootDown;
    [SerializeField] private GameObject arrowprefab;
    [SerializeField] private GameObject playerArrow;
    [SerializeField] private Vector2 playerTransform;

    private void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
    }

    private void Start()
    {
        IdShootX = Animator.StringToHash("ShootX");
        IdShootUp = Animator.StringToHash("ShootUp");
        IdshootDown = Animator.StringToHash("ShootDown");
        playerArrow = GameObject.Find("PlayerTopDown");
    }

    public void PerformAttack(Vector2 direction)
    {
        // Calcula qué animación usar
        float absX = Mathf.Abs(direction.x);
        float absY = Mathf.Abs(direction.y);
        playerTransform = playerArrow.transform.position;
        GameObject arrow = Instantiate(arrowprefab,playerTransform,Quaternion.identity);

        ArrowController arrowController = arrow.GetComponent<ArrowController>();
        arrowController.SetDirection(direction);
        // 2. Calcular rotación según la dirección
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);


        if (absX > absY)
        {
            // Horizontal
            animator.Play(IdShootX, 0, 0);
        }
        else if (direction.y > 0)
        {
            animator.Play(IdShootUp, 0, 0);
        }
        else
        {
            animator.Play(IdshootDown, 0, 0);
        }
    }

    public void StopAttack()
    {

    }
}
