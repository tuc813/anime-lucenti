using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Animator animator;
    private PlayerHealth playerHealth;
    private bool isDead = false;

    public GameObject gameOverUI;
    public float restartDelay = 3f;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (!isDead && playerHealth.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);

        GetComponent<PlayerMovementWithRoll>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<PlayerHealing>().enabled = false;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Invoke("RestartScene", restartDelay);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
