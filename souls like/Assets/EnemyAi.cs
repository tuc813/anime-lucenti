using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float sightRange = 10f;
    public float attackRange = 2f;
    public int damage = 20;
    public LayerMask playerLayer;

    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Rileva il player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Ray ray = new Ray(transform.position + Vector3.up, directionToPlayer);

        if (Physics.Raycast(ray, out RaycastHit hit, sightRange, playerLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                // Se è lontano -> cammina
                if (distance > attackRange)
                {
                    agent.SetDestination(player.position);
                    animator.SetBool("IsWalking", true);
                    animator.SetBool("IsAttacking", false);
                }
                // Se è vicino -> attacca
                else
                {
                    agent.ResetPath();
                    transform.LookAt(player);
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsAttacking", true);

                    // Danno (qui puoi aggiungere un cooldown)
                    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                        playerHealth.TakeDamage(damage);
                }
            }
            else
            {
                StopMoving();
            }
        }
        else
        {
            StopMoving();
        }
    }

    void StopMoving()
    {
        agent.ResetPath();
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", false);
    }

    public void Die()
    {
        isDead = true;
        agent.ResetPath();
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsDead", true);
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }
}
