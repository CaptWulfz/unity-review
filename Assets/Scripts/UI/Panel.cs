using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    [SerializeField] GameObject textContainer;
    [SerializeField] GameObject textReference;

    public void Setup(string[] textWords = null)
    {
        for (int i = 0; i < textWords.Length; i++)
        {
            GameObject textObject = Instantiate(this.textReference, this.textContainer.transform);
            textObject.GetComponent<Text>().text = textWords[i];
            textObject.SetActive(true);
        }
    }
}
