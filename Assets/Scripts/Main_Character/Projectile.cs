using UnityEngine;


public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifespawn = 2;
    public float speed;

    [Header("Combat Settings")]
    public LayerMask enemyLayer;
    public int damage;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    void Start()
    {
        rb.linearVelocity = direction * speed;
        RotateWater();
        Destroy(gameObject, lifespawn);
    }

    private void RotateWater()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<EnemyHealth>().ChangeHealth(-damage);
            collision.gameObject.GetComponent<Enemy_Knockback>().Knockback(transform, knockbackForce, knockbackTime, stunTime);
            Destroy(gameObject); 
        }
    }
}