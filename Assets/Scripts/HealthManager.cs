using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Tooltip("Vida maxima del personaje")]
    public int maxHealth;
    [SerializeField]
    private int currentHealth;

    void Start()
    {
        UpdateMaxHealth(maxHealth);
    }

    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = newMaxHealth;
    }
}
