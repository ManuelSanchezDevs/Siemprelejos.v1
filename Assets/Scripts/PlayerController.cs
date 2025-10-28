// 08/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed;
    [SerializeField] private AttackManager attackScript; // Referencia al script de ataque
    
    private InputActionMap playerBasic;
    private InputAction attackAction;
    private InputAction moveAction;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Transform playerTransform;

    private int lastDirectionX = 1;
    private int idValueX, idValueY, idIsMoving;

    private void Awake()
    {
        if (!playerInput) playerInput = GetComponent<PlayerInput>();
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!animator) animator = GetComponent<Animator>();
    }

    private void Start()
    {
        foreach (var actionMap in playerInput.actions.actionMaps)
        {
            actionMap.Disable();
        }

        playerBasic = playerInput.actions.FindActionMap("Player");

        if (playerBasic != null)
        {
            playerBasic.Enable();
        }
        else
        {
            Debug.LogWarning("No se encontró el mapa de acciones 'Player'. Verifica tu archivo Controls.inputactions.");
        }
        playerTransform = transform;
        idValueX = Animator.StringToHash("ValueX");
        idValueY = Animator.StringToHash("ValueY");
        idIsMoving = Animator.StringToHash("isMoving");
    }

    private void OnEnable()
    {
        playerBasic = playerInput.actions.FindActionMap("Player");
        moveAction = playerBasic.FindAction("Move");
        attackAction = playerBasic.FindAction("Attack");
        attackAction.performed += Attack;
        attackAction.canceled += StopAttack;
        playerBasic.Enable();
    }

    private void OnDisable()
    {
        attackAction.performed -= Attack;
        attackAction.canceled -= StopAttack;
        playerBasic.Disable();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        attackScript.PerformAttack();
    }

    private void StopAttack(InputAction.CallbackContext context)
    {
        attackScript.StopAttack();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        direction = moveAction.ReadValue<Vector2>();

        Vector2 currentVelocity = rb.linearVelocity;

        if (currentVelocity != Vector2.zero)
        {
            animator.SetFloat(idValueX, Mathf.Abs(currentVelocity.x));
            animator.SetFloat(idValueY, currentVelocity.y);
            animator.SetBool(idIsMoving, true);

            if (direction.x != 0)
                lastDirectionX = direction.x > 0 ? 1 : -1;
        }
        else
        {
            animator.SetBool(idIsMoving, false);
        }
        Flip();
    }

    private void Flip()
    {
        playerTransform.localScale = new Vector3(lastDirectionX, 1, 1);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
    }
}
