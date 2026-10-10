using UnityEngine;

public class RandomIdleSpeed : StateMachineBehaviour
{
    [SerializeField] private float minSpeed = 0.9f;
    [SerializeField] private float maxSpeed = 1.1f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed = Random.Range(minSpeed, maxSpeed);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed = 1f;
    }
}