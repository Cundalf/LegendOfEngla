using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [Tooltip("Tiempo que tarda en revivir el personaje")]
    public float timeToRevivePlayer;

    [Tooltip("Daño provocado")]
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.name.Equals("Player") )
        {
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }
}
