using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Tooltip("Vida maxima del personaje")]
    public int maxHealth;
    [SerializeField]
    private int currentHealth;

    public int Health
    {
        get
        {
            return currentHealth;
        }
    }

    public bool flashActive;
    public float flashLength;
    private float flashCounter;
    private SpriteRenderer _characterRenderer;

    public int expWhenDefeated;

    void Start()
    {
        _characterRenderer = GetComponent<SpriteRenderer>();
        UpdateMaxHealth(maxHealth);
    }

    private void Update()
    {
        if( flashActive )
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter > flashLength * 0.66f)
            {
                ToggleColor(false);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                ToggleColor(true);
            }
            else if (flashCounter > 0)
            {
                ToggleColor(false);
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<PlayerController>().canMove = true;
                ToggleColor(true);
                flashActive = false;
            }
        }
    }

    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);

            if(gameObject.tag.Equals("Enemy"))
            {
                GameObject.Find("Player").GetComponent<CharacterStats>().addExperience(expWhenDefeated);
            }
        }

        if(flashLength > 0)
        {
            flashActive = true;
            flashCounter = flashLength;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<PlayerController>().canMove = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = newMaxHealth;
    }

    private void ToggleColor(bool visible)
    {
        _characterRenderer.color = new Color(_characterRenderer.color.r,
                                            _characterRenderer.color.g,
                                            _characterRenderer.color.b,
                                            (visible ? 1 : 0) );

    }

}
