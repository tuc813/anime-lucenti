using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementWithRoll : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rollSpeed = 10f;
    public float rollDuration = 0.5f;
    public Transform cameraTransform;
    public Slider staminaBar; // Stamina slider UI
    public float currentStamina = 100f;  // Stamina corrente
    public float maxStamina = 100f;      // Massima stamina
    public float staminaRechargeRate = 5f; // Velocità di ricarica della stamina
    public float rollStaminaCost = 20f; // Costo della stamina durante il roll

    private Animator animator;

    CharacterController controller;

    Vector3 moveDirection;
    bool isRolling = false;
    float rollTimer = 0f;
    bool isInvincible = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (isRolling)
        {
            Roll();
        }
        else
        {
            Move();
            if (Input.GetKeyDown(KeyCode.Space) && currentStamina >= rollStaminaCost)
                StartRoll();
        }

        // Aggiorna lo slider della stamina ogni frame
        if (staminaBar != null)
        {
            staminaBar.value = currentStamina / maxStamina;
        }

        // Recupero stamina mentre il player si muove
        if (moveDirection.magnitude > 0.1f && currentStamina < maxStamina)
        {
            currentStamina += staminaRechargeRate * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
        else if (moveDirection.magnitude == 0 && currentStamina < maxStamina)
        {
            // Se il player è fermo, la stamina continua a ricaricarsi lentamente
            currentStamina += staminaRechargeRate * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        moveDirection = (camForward * v + camRight * h).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            // Movimento
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
            animator.SetBool("IsMoving", true);
            // Rotazione verso il movimento
            Quaternion targetRot = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    void StartRoll()
    {
        if (moveDirection.magnitude == 0 || currentStamina <= rollStaminaCost) return;

        isRolling = true;
        rollTimer = rollDuration;
        isInvincible = true;
        animator.SetBool("IsRolling", true);
        // Riduce la stamina durante il roll
        currentStamina -= rollStaminaCost;
    }

    void Roll()
    {
        controller.Move(moveDirection * rollSpeed * Time.deltaTime);
        rollTimer -= Time.deltaTime;

        if (rollTimer <= 0f)
        {
            isRolling = false;
            isInvincible = false;
            animator.SetBool("IsRolling",false);
        }
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public bool IsRolling()
    {
        return isRolling;
    }

    // Metodo per ottenere la stamina corrente
    public float GetCurrentStamina()
    {
        return currentStamina;
    }

    // Metodo per ottenere la stamina come valore da 0 a 1 per lo slider
    public float GetStamina01()
    {
        return currentStamina / maxStamina;
    }
}
