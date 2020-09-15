using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextAnimator : MonoBehaviour
{
    public bool finished;
    public float pauseTime = 0.1f;
    private TextMeshProUGUI _text;
    public bool skip;
    
    void Start()
    {
        skip = false;
        finished = true;
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string txt)
    {
        finished = false;
        _text.text = "";
        StartCoroutine(TypeLetters(txt));
    }

    IEnumerator TypeLetters(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            if(skip)
            {
                _text.text = text;
                skip = false;
                break;
            }

            _text.text += letter;
            yield return new WaitForSeconds(pauseTime);
        }

        finished = true;
    }
}
