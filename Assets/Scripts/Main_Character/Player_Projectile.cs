using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Projectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float characterRadius = 0.5f; //launch point
    private Camera mainCamera;

    public float shootCooldown = 0.5f;
    private float shootTimer;

    public Animator anim;
    public AryasPlayerMovement playerMovement;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && shootTimer <= 0)
        {   
            playerMovement.isShooting = true;
            anim.SetBool("IsShooting", true);
            //Shoot();
        }
    }

    private void Shoot()
    {
        //Get mouse position
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));
        mouseWorldPosition.z = 0;
        
        //Calculate direction
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;
        
        //Calculate spawn position 
        Vector2 spawnPosition = (Vector2)transform.position + direction * characterRadius;
        
        //Create projectile
        Projectile water = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity).GetComponent<Projectile>();
        water.direction = direction;
        
        shootTimer = shootCooldown;
        Debug.Log($"Shooting from {spawnPosition} towards {direction}");

        anim.SetBool("IsShooting", false);
        playerMovement.isShooting = false;
    }

    
}