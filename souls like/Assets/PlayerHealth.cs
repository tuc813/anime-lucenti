using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int maxCures = 3;  // Numero massimo di cure
    public int currentHeals;  // Quante cure hai

    public Text healthText;  // Riferimento al testo della salute
    public Text curesText;   // Riferimento al testo delle cure

    void Start()
    {
        currentHealth = maxHealth;
        currentHeals = maxCures;  // Imposta le cure iniziali
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && currentHeals > 0)  // Usa cura se ci sono cure disponibili
        {
            UseCure();
        }
    }

    // Metodo per curare il giocatore
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;  // Non superare la salute massima
        currentHeals--;  // Decrementa le cure disponibili
    }

    // Metodo per infliggere danno al giocatore
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;  // Impedisce che la salute scenda sotto zero
            Die();  // Metodo per gestire la morte del giocatore
        }
    }

    // Gestione della morte del giocatore (puoi personalizzarlo)
    void Die()
    {
        Debug.Log("Player has died!");
        // Puoi aggiungere altre azioni, come disabilitare il player, far partire una scena di game over, ecc.
    }

    // Usa la cura (metodo che viene chiamato quando si preme H)
    void UseCure()
    {
        if (currentHeals > 0)
        {
            Heal(20);  // Cura il giocatore di 20 punti (modifica come vuoi)
        }
    }

    public void UpdateUI(Text healthText, Text curesText)
    {
        healthText.text = "Health: " + currentHealth.ToString();  // Aggiorna il testo della salute
        curesText.text = "Cures: " + currentHeals.ToString();  // Aggiorna il testo delle cure
    }
}
