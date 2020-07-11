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
            case ItemType.SPECIAL_ITEMS:
                QuestItem item = FindObjectOfType<ItemsManager>().QuestItemAt(itemIndex);
                Debug.Log("Objeto: " + item.itemName);
                break;
        }
    }
}
