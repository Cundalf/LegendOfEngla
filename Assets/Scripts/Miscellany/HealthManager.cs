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
        set
        {
            if(value < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    public bool flashActive;
    public float flashLength;
    private float flashCounter;
    private SpriteRenderer _characterRenderer;

    public int expWhenDefeated;

    private QuestEnemy quest;
    private QuestManager questManager;

    void Start()
    {
        _characterRenderer = GetComponent<SpriteRenderer>();
        UpdateMaxHealth(maxHealth);
        quest = GetComponent<QuestEnemy>();
        questManager = FindObjectOfType<QuestManager>();
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
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.HIT);

        Health -= damage;
        if (Health <= 0)
        {
            gameObject.SetActive(false);

            if (gameObject.tag.Equals("Enemy"))
            {
                GameObject.Find("Player").GetComponent<CharacterStats>().addExperience(expWhenDefeated);
                questManager.enemyKilled = quest;
            }


            if (gameObject.tag.Equals("Player"))
            {
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIE);
                //TODO: Implementar GameOver
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
        Health = newMaxHealth;
    }

    private void ToggleColor(bool visible)
    {
        _characterRenderer.color = new Color(_characterRenderer.color.r,
                                            _characterRenderer.color.g,
                                            _characterRenderer.color.b,
                                            (visible ? 1 : 0) );

    }

}
