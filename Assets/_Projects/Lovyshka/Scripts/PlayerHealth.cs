using UnityEngine;

public class PlayerHealth : Damageable
{
    [SerializeField] private int _maximumHealth = 100;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maximumHealth;
    }

    public override void TakeDamage(int damage)
    {
        if (damage <= 0) 
            return;

        _currentHealth -= damage;

        if (_currentHealth < 0)
            _currentHealth = 0;

        Debug.Log($"Игрок получил {damage} урона. Здоровье: {_currentHealth}");

        if (_currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Debug.Log("Игрок погиб.");
    }
}
