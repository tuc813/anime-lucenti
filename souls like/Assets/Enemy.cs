using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Transform player;
    public float attackRange = 2f;
    public int damage = 10;
    public float attackCooldown = 2f;
    public float deathDelay = 2f;

    private Animator animator;
    private NavMeshAgent agent;
    float cooldownTimer = 0f;
    bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead || player == null) return;

        agent.SetDestination(player.position);
        float distance = Vector3.Distance(transform.position, player.position);

        animator.SetBool("IsRunning", agent.velocity.magnitude > 0.1f);

        if (distance <= attackRange && cooldownTimer <= 0f)
        {
            animator.SetBool("IsAttacking", true);

            PlayerHealth health = player.GetComponent<PlayerHealth>();
            PlayerMovementWithRoll movement = player.GetComponent<PlayerMovementWithRoll>();

            if (health != null && movement != null && !movement.IsRolling())
            {
                health.TakeDamage(damage);
                cooldownTimer = attackCooldown;
            }
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }

        cooldownTimer -= Time.deltaTime;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("IsDead", true);

        yield return new WaitForSeconds(deathDelay);

        Destroy(gameObject);
    }
}
