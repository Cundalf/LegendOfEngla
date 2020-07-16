using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTemp : MonoBehaviour
{
    void Start()
    {
        string[] dialog = {"Los controles estan en desarrollo. \n Te moves con las flechas del teclado. \n Con click atacas y usas la interfaz. \n Con click derecho ejecutas acciones." };
        FindObjectOfType<DialogManager>().ShowDialogue(dialog);
        gameObject.SetActive(false);
    }
}
