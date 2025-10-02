using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Animator anim;


    public void Attack()
    {
        anim.SetBool("IsAttacking", true);
    }

    public void FinishAttacking()
    {
        anim.SetBool("IsAttacking", false);
    }
}
