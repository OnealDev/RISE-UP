using UnityEngine;

public class DemonAttackHitbox : MonoBehaviour 
{
    public string attackType; //Set in Inspector
    private DemonCombat parentCombat;
    
    void Start()
    {
        parentCombat = GetComponentInParent<DemonCombat>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (parentCombat != null)
        {
            parentCombat.OnHitboxTrigger(collision, attackType);
        }
    }
}