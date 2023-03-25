using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    
    private void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(50);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void Die()
    {
        _playerController.enabled = false;
        _playerController.Animator.SetTrigger("Death");
    }
}
