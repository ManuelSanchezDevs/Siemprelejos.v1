using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D arrowRB;
    [SerializeField] private float velocity;
    private Vector2 arrowDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        arrowRB = GetComponent<Rigidbody2D>();
        arrowDirection = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        arrowRB.linearVelocity = arrowDirection * velocity;
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.collider.CompareTag("collision"))
    //    { 
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("collision"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        arrowDirection = dir.normalized;
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
