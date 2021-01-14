using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    [SerializeField]
    private SaveManager buttonControl;

    private string pathString;

    public void SetText(string text)
    {
        myText.text = text;
        pathString = text;
    }

    public void OnClick()
    {
        buttonControl.ButtonClicked(pathString);
    }
}
