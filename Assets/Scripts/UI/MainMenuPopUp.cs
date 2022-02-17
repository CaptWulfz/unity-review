using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPopUp : PopUp
{
    [SerializeField] Text displayText;
    public void Setup(string Text)
    {
        this.displayText.text = Text;
    }
}
