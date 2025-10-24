using UnityEngine;

public class Slime_Combat : MonoBehaviour
{
    public int damage = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent <AryasHealthScript>().ChangeHealth(-damage);
    }
}
