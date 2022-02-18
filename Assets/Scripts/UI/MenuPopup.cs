using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPopup : Popup
{
    [SerializeField] Text displayText;

    public void Setup(string text){

        this.displayText.text = text;

    }   
}
