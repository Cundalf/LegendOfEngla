using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;
    public Slider playerExpBar;
    public TMP_Text playerHealthText;
    public TMP_Text playerLevelText;
    public HealthManager playerHealthManager;
    public CharacterStats playerStats;
    public GameObject inventoryPanel, menuPanel;
    public Button inventoryButton;
    public Image playerAvatar;
    private WeaponManager weaponManager;
    private ItemsManager itemsManager;

    void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
        inventoryPanel.SetActive(false);
        menuPanel.SetActive(false);
    }

    // TODO: Pasar a subscripcion de eventos para optimizar
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        playerHealthBar.maxValue = playerHealthManager.maxHealth;
        playerHealthBar.value = playerHealthManager.Health;

        StringBuilder stringBuilder = new StringBuilder()
            .Append("HP: ")
            .Append(playerHealthManager.Health)
            .Append(" / ")
            .Append(playerHealthManager.maxHealth);

        playerHealthText.text = stringBuilder.ToString();

        stringBuilder = new StringBuilder().Append("Nivel: ").Append(playerStats.level);
        playerLevelText.text = stringBuilder.ToString();

        if(playerStats.level >= playerStats.expToLevelUp.Length)
        {
            playerExpBar.enabled = false;
            return;
        }

        playerExpBar.maxValue = playerStats.expToLevelUp[playerStats.level];
        playerExpBar.minValue = playerStats.expToLevelUp[playerStats.level - 1];
        playerExpBar.value = playerStats.exp;
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        menuPanel.SetActive(inventoryPanel.activeInHierarchy);

        if(inventoryPanel.activeInHierarchy)
        {
            foreach (Transform tempTransform in inventoryPanel.transform) {
                Destroy(tempTransform.gameObject);
            };
            FillInventory();
        }
    }

    public void FillInventory()
    {

        // WEAPONS
        List<GameObject> items = weaponManager.GetAllWeapons();
        int i = 0;

        foreach ( GameObject item in items)
        {
            AddItemToInventory(item, InventoryButton.ItemType.WEAPON, i);
            i++;
        }

        // SPECIAL ITEMS
        items = itemsManager.GetQuestItems();
        i = 0;

        foreach (GameObject item in items)
        {
            AddItemToInventory(item, InventoryButton.ItemType.SPECIAL_ITEMS, i);
            i++;
        }
    }

    private void AddItemToInventory(GameObject item, InventoryButton.ItemType type, int idx)
    {
        Button tempB = Instantiate(inventoryButton, inventoryPanel.transform);
        tempB.GetComponent<InventoryButton>().type = type;
        tempB.GetComponent<InventoryButton>().itemIndex = idx;
        tempB.onClick.AddListener(() => tempB.GetComponent<InventoryButton>().ActivateButton());
        tempB.image.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    public void ShowOnly(int type)
    {
        foreach (Transform t in inventoryPanel.transform)
        {
            Debug.Log((int)t.GetComponent<InventoryButton>().type);
            t.gameObject.SetActive((int)t.GetComponent<InventoryButton>().type == type);
        }
    }

    public void ShowAll()
    {
        foreach (Transform t in inventoryPanel.transform)
        {
            t.gameObject.SetActive(true);
        }
    }

    public void ChangeAvatarImage(Sprite sprite)
    {
        playerAvatar.sprite = sprite;
    }
}
