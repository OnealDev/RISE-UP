using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Knockback(Transform playerTransform, float knockbackForce)
    {
        Vector2 direction = ((Vector2)(transform.position - playerTransform.position)).normalized;
        rb.linearVelocity = direction * knockbackForce;
        Debug.Log("knockback applied.");
    }
}