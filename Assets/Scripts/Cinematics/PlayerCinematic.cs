using UnityEngine;

public class PlayerCinematic : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private GameObject player;
    [SerializeField] public Animator animator;
    [SerializeField] private int indexDialogue;
    [SerializeField] private Rigidbody2D rb;
    public  float duration;

    void Start()
    {
        player = SetSexPlayer.currentPlayer;
        animator = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        indexDialogue = dialogue.Index;
        if (indexDialogue == 3)
        {
            rb.linearVelocity = Vector2.right * 3f;
            animator.SetFloat("ValueX",1);
            animator.SetBool("isMoving",true);
            duration -= Time.deltaTime;
        }
        if (duration <= 0)
        {
            animator.SetBool("isMoving",false);
            rb.linearVelocity = Vector2.zero;
        }
    }


}
