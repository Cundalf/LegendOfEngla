using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public int questID;
    public bool startPoint, endPoint;
    public bool automaticCatch;

    private QuestManager questManager;
    private bool playerInZone;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void Update()
    {
        if (!playerInZone) return;

        if ((automaticCatch) || (!automaticCatch && Input.GetMouseButtonDown(1)))
        {
            Quest quest = questManager.QuestWithID(questID);
            if(quest == null)
            {
                Debug.LogErrorFormat("La miision con ID {0} no existe", questID);
                return;
            }

            
            if (!quest.questCompleted)
            {
                if(startPoint)
                {
                    if(!quest.gameObject.activeInHierarchy)
                    {
                        quest.gameObject.SetActive(true);
                        quest.StartQuest();
                    }
                }
                       
                if(endPoint)
                {
                    if (quest.gameObject.activeInHierarchy)
                    {
                        quest.gameObject.SetActive(false);
                        quest.CompleteQuest();
                    }
                }
            }    
        } 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInZone = false;
        }
    }
}
