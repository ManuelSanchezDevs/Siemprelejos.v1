//using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInputPlatform : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerInput playerInput;
    private InputActionMap playerBasic;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private Animator animator;
    private Rigidbody2D rb;
    private Transform spriteTransform;

    [Header("Values")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;
    private Vector2 direction;
    private int checkDirection = 1;

    // Animator hashes
    private int idValueX, idIsMoving, idIsJumping, idIsFalling;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteTransform = transform; // si tienes un hijo Sprite, arrástralo aquí
    }

    private void Start()
    {
        idValueX = Animator.StringToHash("ValueX");
        idIsMoving = Animator.StringToHash("isMoving");
        idIsJumping = Animator.StringToHash("isJumping");
        idIsFalling = Animator.StringToHash("isFalling");
    }

    private void OnEnable()
    {
        playerBasic = playerInput.actions.FindActionMap("Player");
        moveAction = playerBasic.FindAction("Move");
        attackAction = playerBasic.FindAction("Attack");
        jumpAction = playerBasic.FindAction("Jump");

        attackAction.performed += Attack;
        attackAction.canceled += StopAttack;
        jumpAction.performed += Jump;

        playerBasic.Enable();
    }

    private void OnDisable()
    {
        attackAction.performed -= Attack;
        attackAction.canceled -= StopAttack;
        jumpAction.performed -= Jump;

        playerBasic.Disable();
    }

    private void Update()
    {
        direction = moveAction.ReadValue<Vector2>();
        HandleAnimations();
        Flip();
    }

    private void FixedUpdate()
    {
        // Movimiento horizontal solo, Y controlada por física
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
    }

    private void HandleAnimations()
    {
        animator.SetFloat(idValueX, Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool(idIsMoving, Mathf.Abs(direction.x) > 0.01f);
        animator.SetBool(idIsJumping, rb.linearVelocity.y > 0.1f);
        animator.SetBool(idIsFalling, rb.linearVelocity.y < -0.1f);
    }

    private void Flip()
    {
        if (checkDirection * direction.x < 0)
        {
            spriteTransform.localScale = new Vector3(-spriteTransform.localScale.x, 1, 1);
            checkDirection *= -1;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        print("saltofunciona");
        // Saltar solo si está tocando suelo
        if (IsGrounded())
        {
            rb.linearVelocity  = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        print("Attack");
        // weaponManager?.EquipBow(); // descomenta si tienes WeaponManager
    }

    private void StopAttack(InputAction.CallbackContext context)
    {
        print("StopAttack");
    }

    // Función simple para detectar si está en el suelo
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }
}
