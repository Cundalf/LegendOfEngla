using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public enum ItemType { WEAPON, ITEM, ARMOR, RING, SPECIAL_ITEMS }
    public int itemIndex;
    public ItemType type;

    public void ActivateButton()
    {
        switch (type) 
        {
            case ItemType.WEAPON:
                FindObjectOfType<WeaponManager>().ChangeWeapon(itemIndex);
                break;
            case ItemType.ITEM:
                Debug.Log("Ejecuccion de consumible (pendiente)");
                break;
            case ItemType.ARMOR:
                Debug.Log("Ejecuccion de armadura (pendiente)");
                break;
            case ItemType.RING:
                Debug.Log("Ejecuccion de anillos (pendiente)");
                break;
        }

        ShowDescription();
    }
    public void ShowDescription()
    {
        string desc = "";
        switch (type)
        {
            case ItemType.WEAPON:
                desc = FindObjectOfType<WeaponManager>().GetWeaponAt(itemIndex).WeaponName;
                break;
            case ItemType.ITEM:
                desc = "Item consumible";
                break;
            case ItemType.ARMOR:
                desc = FindObjectOfType<WeaponManager>().GetArmorAt(itemIndex).name;
                break;
            case ItemType.RING:
                desc = FindObjectOfType<WeaponManager>().GetRingAt(itemIndex).name;
                break;
            case ItemType.SPECIAL_ITEMS:
                desc = FindObjectOfType<ItemsManager>().QuestItemAt(itemIndex).itemName;
                break;
        }

        FindObjectOfType<UIManager>().inventoryText.text = desc;
    }

    public void ClearDescription()
    {
        FindObjectOfType<UIManager>().inventoryText.text = "";
    }
}
