using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestItem : MonoBehaviour
{
    public int questID;
    public string itemName;
    private QuestManager questManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            questManager = FindObjectOfType<QuestManager>();

            Quest q = questManager.QuestWithID(questID);
            if(q == null)
            {
                Debug.LogErrorFormat("La mision con id {0} no existe", questID);
                return;
            }

            if(q.gameObject.activeInHierarchy && !q.questCompleted)
            {
                questManager.itemCollected = this;
                gameObject.SetActive(false);
            }
        }
    }
}
