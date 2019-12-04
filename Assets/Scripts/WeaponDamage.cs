using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [Tooltip("Daño provocado")]
    public int damage;

    public GameObject bloodAnim;
    public GameObject damageCanvas;
    private GameObject hitPoit;

    private void Start()
    {
        hitPoit = transform.Find("HitPoint").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            if (bloodAnim != null && hitPoit != null)
            {
                Destroy( Instantiate(bloodAnim, 
                        hitPoit.transform.position, 
                        hitPoit.transform.rotation)
                    , 0.5f );
            }

            var clone = (GameObject) Instantiate(
                damageCanvas, 
                hitPoit.transform.position, 
                Quaternion.Euler(Vector3.zero)
            );
            clone.GetComponent<DamageNumber>().damagePoints = damage;

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }

}
