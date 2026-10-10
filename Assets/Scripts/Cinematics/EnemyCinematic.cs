using System.Collections;
using UnityEngine;

public class EnemyCinematic : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private Dialogue Dialogue;
    [SerializeField] private float duration;
    [SerializeField] Animator animator;
    [SerializeField] Animator playerAnimator;
    [SerializeField] private bool moveOnIndex6;
    [SerializeField] private bool isAttacking;
    public PlayerCinematic playerCinematic;
    public bool isMove = true;
    private SpriteRenderer sr;
    private int lastIndex = -1;

    private Rigidbody2D rb;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Dialogue = FindAnyObjectByType<Dialogue>();
    }

    private void Start()
    {
        playerCinematic = GameObject.Find("Player").GetComponent<PlayerCinematic>();
        //playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        playerAnimator = playerCinematic.GetComponent<Animator>();
    }

    void Update()
    {
        DialogueIndexer();
    }

    private void DialogueIndexer()
    {
        if(Dialogue.Index != lastIndex)
        {
            if (Dialogue.Index == 1)
            {
                MoveEnemy();
            }

            if (Dialogue.Index == 5 && moveOnIndex6)
            {
                MoveEnemy();
                
            }
            
            lastIndex = Dialogue.Index;
        }
        Flip();
    }

    private void Flip()
    {
        if(rb.linearVelocityX < 0)
        {
            sr.flipX = true;
        }
        else if(rb.linearVelocityX > 0)
        {
            sr.flipX = false;
        }
    }

    private void MoveEnemy()
    {
        CancelInvoke("StopEnemy");

        animator.SetFloat("Walk", 1f);
        rb.linearVelocity = Vector2.left * velocity;

        Invoke("StopEnemy",duration);
    }

    private void StopEnemy()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetFloat("Walk", 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAttacking)
        {
            isAttacking = true;
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(DoubleAttack());
        }
    }

    private IEnumerator DoubleAttack()
    {
        animator.Play("Goblin_Attack",0,0);
        playerCinematic.animator.Play("PlayerDamage");
        //playerCinematic.animator.SetBool("Damage",true);
        yield return new WaitForSeconds(1f);
        animator.Play("Goblin_Attack",0,0);
        yield return new WaitForSeconds(1f);
        animator.Play("Goblin_Iddle");
        isAttacking = false;
    }
}
