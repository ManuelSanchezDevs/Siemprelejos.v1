using UnityEngine;

public class IdleStretchSquash : MonoBehaviour
{
    [SerializeField] private float stretchAmount = 0.05f; // cuánto se estira
    [SerializeField] private float speed = 2f;            // velocidad de la animación

    private Vector3 originalScale;
    private Rigidbody2D rb;

    void Start()
    {
        originalScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsIdle())
        {
            float stretch = Mathf.Sin(Time.time * speed) * stretchAmount;
            transform.localScale = new Vector3(
                originalScale.x + stretch,
                originalScale.y - stretch,
                originalScale.z
            );
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    bool IsIdle()
    {
        return rb.linearVelocity.magnitude < 0.01f;
    }











}
