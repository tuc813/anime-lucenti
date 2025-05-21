using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 25;
    public float attackRange = 2f;
    public LayerMask enemyLayer;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // <--- aggiunto
    }

    // Aggiungi una variabile per PlayerMovementWithRoll
    public PlayerMovementWithRoll playerMovement;  // Riferimento per ottenere la stamina
    public float attackStaminaCost = 15f;          // Quanto costa un attacco in stamina

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Tasto sinistro del mouse per attaccare
        {
            animator.SetBool("IsAttacking", true);
            // Controlla se c'è abbastanza stamina
            if (playerMovement != null && playerMovement.GetCurrentStamina() >= attackStaminaCost)
            {
                // Esegui l'attacco
                RaycastHit hit;

                if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, attackRange, enemyLayer))
                {
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                }

                // Riduci la stamina dopo l'attacco
                playerMovement.currentStamina -= attackStaminaCost;

                // Assicurati che la stamina non scenda sotto 0
                if (playerMovement.currentStamina < 0)
                    playerMovement.currentStamina = 0;
            }
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }   
}
