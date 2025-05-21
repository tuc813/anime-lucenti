using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider staminaBar;
    public Text healCountText;

    public PlayerHealth playerHealth;
    public PlayerMovementWithRoll playerMovement;

    void Update()
    {
        if (playerHealth != null)
        {
            healthBar.value = (float)playerHealth.currentHealth / playerHealth.maxHealth;
            healCountText.text = "Cure: " + playerHealth.currentHeals;
        }

        if (playerMovement != null)
        {
            staminaBar.value = playerMovement.GetStamina01(); // ritorna valore da 0 a 1
        }
    }
}
