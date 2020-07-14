using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour
{
    public string uuid;
    public string newPlaceName = "New Scene Name Here!";
    public bool needsClick = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        teleport(collision.gameObject.name);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        teleport(collision.gameObject.name);
    }

    private void teleport(string collisionName)
    {
        if (collisionName == "Player")
        {
            if (!needsClick || (needsClick && Input.GetMouseButtonDown(0)))
            {
                if (needsClick)
                {
                    SFXManager.SharedInstance.PlaySFX(SFXManager.SFXType.KNOCK);
                }
                
                SceneManager.LoadScene(newPlaceName);
                FindObjectOfType<PlayerController>().nextUUID = uuid;
            }
        }
    }
}