using UnityEngine;
using System.Collections;

public class DemonKnockback : MonoBehaviour
{
     public float knockForce = 7f;
     public float knockDuration = 0.2f;

     private Rigidbody2D rb;
     public bool IsKnocked { get; private set; }

     void Awake()
     {
          rb = GetComponent<Rigidbody2D>();
     }

     public void ApplyKnockback(Vector2 direction)
     {
          if (IsKnocked) return;
          IsKnocked = true;

          rb.linearVelocity = direction * knockForce;

          StartCoroutine(Release());
     }

     IEnumerator Release()
     {
          yield return new WaitForSeconds(knockDuration);

          rb.linearVelocity = Vector2.zero;
          IsKnocked = false;
     }
}
