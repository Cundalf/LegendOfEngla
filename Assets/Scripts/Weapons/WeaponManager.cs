using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private List<GameObject> weapons;
    public int activeWeapon;

    private List<GameObject> armors;
    public int activeArmors;

    private List<GameObject> rings;
    public int activeRing1, activeRing2;

    private void Start()
    {
        weapons = new List<GameObject>();
        
        foreach(Transform weapon in transform) 
        {
            weapons.Add(weapon.gameObject);
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive( false );
        }

        //TODO: Agregar armaduras
        armors = new List<GameObject>();

        //TODO: Agregar anillos
        rings = new List<GameObject>();
    }

    public void ChangeWeapon(int newWeapon) 
    {
        weapons[activeWeapon].SetActive(false);
        weapons[newWeapon].SetActive(true);
        activeWeapon = newWeapon;
        FindObjectOfType<UIManager>().ChangeAvatarImage(weapons[activeWeapon].GetComponent<SpriteRenderer>().sprite);
    }

    public List<GameObject> GetAllWeapons()
    {
        return weapons;
    }

    public List<GameObject> GetAllArmors()
    {
        return armors;
    }

    public List<GameObject> GetAllRings()
    {
        return rings;
    }

    public WeaponDamage GetWeaponAt(int idx)
    {
        return weapons[idx].GetComponent<WeaponDamage>();
    }

    public GameObject GetArmorAt(int idx)
    {
        return armors[idx];
    }

    public GameObject GetRingAt(int idx)
    {
        return rings[idx];
    }
}
