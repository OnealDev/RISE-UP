using UnityEngine;

public class PushableRock : MonoBehaviour
{
     [Header("Push Settings")]
     public float pushForce = 3f;
     public Rigidbody2D rb;

     [Header("Fall Settings")]
     public bool canFall = false;
     public float gravityWhenFalling = 5f;

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
     }

     private void OnCollisionStay2D(Collision2D collision)
     {
          // Always pushable
          if (collision.gameObject.CompareTag("Player"))
          {
               // Direction from player rock
               Vector2 pushDir = (transform.position - collision.transform.position).normalized;
               rb.AddForce(pushDir * pushForce, ForceMode2D.Force);
          }
     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          // Trigger must be tagged "Cliff"
          if (collision.CompareTag("Cliff"))
          {
               canFall = true;
               StartFalling();
          }
     }

     void StartFalling()
     {
          rb.gravityScale = gravityWhenFalling;
          rb.constraints = RigidbodyConstraints2D.None; // Free fall

          Debug.Log("ROCK IS FALLING!");
          Destroy(gameObject, 3f); // Delete after falling
     }
}
