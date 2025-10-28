
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

    private Vector2 lastDirection = Vector2.right; // dirección por defecto

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
        Vector2 attackDir = direction;

        if (attackDir == Vector2.zero)
        {
            attackDir = lastDirection;
        }

            attackScript.PerformAttack(attackDir);
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

            if (direction != Vector2.zero)
                lastDirection = direction;
        }
        else
        {
            animator.SetBool(idIsMoving, false);
        }
        Flip();
    }

    private void Flip()
    {
        float scaleX = lastDirection.x >= 0 ? 1f : -1f;
        playerTransform.localScale = new Vector3(scaleX, 1f, 1f);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
    }
}
