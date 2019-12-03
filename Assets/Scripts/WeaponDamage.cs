using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [Tooltip("Daño provocado")]
    public int damage;

    public GameObject bloodAnim;
    private GameObject hitPoit;
    private GameObject currentBlood;

    private void Start()
    {
        hitPoit = transform.Find("HitPoint").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            if (bloodAnim != null)
            {
                currentBlood = Instantiate(bloodAnim, hitPoit.transform.position, hitPoit.transform.rotation);

                Invoke("DestroyBlood", 0.5f);
            }

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }

    private void DestroyBlood()
    {
        Destroy(currentBlood);
    }
}
