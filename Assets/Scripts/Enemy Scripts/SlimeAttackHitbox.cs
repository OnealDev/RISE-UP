using UnityEngine;

public class SlimeAttackHitbox : MonoBehaviour 
{
    private Slime_Combat parentCombat;
    
    void Start()
    {
        parentCombat = GetComponentInParent<Slime_Combat>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (parentCombat != null)
        {
            parentCombat.OnHitboxTrigger(collision);
        }
    }
}