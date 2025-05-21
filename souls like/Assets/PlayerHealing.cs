using UnityEngine;

public class PlayerHealing : MonoBehaviour
{
    public int healAmount = 30;
    public int maxHeals = 3;
    private Animator animator;
    int healsLeft;

    void Start()
    {
        healsLeft = maxHeals;
        animator = GetComponent<Animator>(); // <--- aggiunto
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && healsLeft > 0)
        {
            GetComponent<PlayerHealth>().Heal(healAmount);
            healsLeft--;
            animator.SetBool("IsHealing", true);
        }
        else
        {
            animator.SetBool("IsHealing", false);
        }
    }
}
