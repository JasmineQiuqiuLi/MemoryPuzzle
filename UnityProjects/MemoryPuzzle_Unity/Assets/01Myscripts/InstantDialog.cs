using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDialog : MonoBehaviour
{
    public GameObject header;
    public GameObject description;

    public void ShowDialogue()
    {
        gameObject.SetActive(true);
    }

    public void SetContent(string header,string description)
    {
        
    }
}
