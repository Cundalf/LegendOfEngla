using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int value;
    private MoneyManager manager;

    void Start()
    {
        manager = FindObjectOfType<MoneyManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Player"))
        {
            manager.AddMoney(value);
            Destroy(gameObject);
        }
    }
}
