using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public TextAnimator txtPrimary;
    public Image img;
    public string[] dialogs;
    public Sprite[] images;
    private int currentDialog;

    private void Start()
    {
        currentDialog = 0;
        ShowDialog();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (txtPrimary.finished)
            {
                currentDialog++;
                if (currentDialog >= dialogs.Length)
                {
                    SceneManager.LoadScene("VillageScene");
                }
                else
                {
                    ShowDialog();
                }
            }
            else
            {
                txtPrimary.skip = true;
            }
        }
    }

    private void ShowDialog()
    {
        img.sprite = images[currentDialog];
        txtPrimary.SetText(dialogs[currentDialog]);
    }
}
