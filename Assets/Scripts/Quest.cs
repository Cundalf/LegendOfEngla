using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    public int questID;
    public bool questCompleted;
    public string title;
    public string startText;
    public string completeText;

    public bool needsItem;
    public List<QuestItem> itemsNeeded;

    public bool killsEnemy;
    public List<QuestEnemy> enemiesNeeded;
    public List<int> numberOfEnemies;

    private QuestManager questManager;

    public Quest nextQuest;

    public void StartQuest()
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(title + "\n" + startText);

        if(needsItem)
        {
            ActivateItems();
        }

        if (killsEnemy)
        {
            ActivateEnemies();
        }
    }

    void ActivateItems()
    {
        Object[] qItems = Resources.FindObjectsOfTypeAll<QuestItem>();
        foreach (QuestItem item in qItems)
        {
            if (item.questID == questID)
            {
                item.gameObject.SetActive(true);
            }
        }
    }

    void ActivateEnemies()
    {
        Object[] qEnemies = Resources.FindObjectsOfTypeAll<QuestEnemy>();
        foreach (QuestEnemy enemy in qEnemies)
        {
            if (enemy.questID == questID)
            {
                enemy.gameObject.SetActive(true);
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (needsItem)
        {
            ActivateItems();
        }

        if (killsEnemy)
        {
            ActivateEnemies();
        }
    }

    public void CompleteQuest()
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(title + "\n" + completeText);
        questCompleted = true;

        if(nextQuest != null)
        {
            Invoke("ActivateNextQuest", 5.0f);
        }

        gameObject.SetActive(false);
    }

    void ActivateNextQuest()
    {
        nextQuest.gameObject.SetActive(true);
        nextQuest.StartQuest();
    }

    private void Update()
    {
        if (needsItem && questManager.itemCollected != null)
        {
            for (int i = 0; i < itemsNeeded.Count; i++)
            {
                if (itemsNeeded[i].itemName == questManager.itemCollected.itemName)
                {
                    questManager.itemCollected = null;
                    itemsNeeded.RemoveAt(i);
                    break;
                }
            }

            if (itemsNeeded.Count == 0)
            {
                CompleteQuest();
            }
        }

        if (killsEnemy && questManager.enemyKilled != null) 
        {
            for(int i = 0; i < enemiesNeeded.Count; i++)
            {
                if(enemiesNeeded[i].enemyName == questManager.enemyKilled.enemyName)
                {
                    numberOfEnemies[i]--;
                    if (numberOfEnemies[i] <= 0)
                    {
                        enemiesNeeded.RemoveAt(i);
                        numberOfEnemies.RemoveAt(i);   
                    }
                    questManager.enemyKilled = null;
                    break;
                }
            }

            if(enemiesNeeded.Count == 0)
            {
                CompleteQuest();
            }
        }
    }
}
