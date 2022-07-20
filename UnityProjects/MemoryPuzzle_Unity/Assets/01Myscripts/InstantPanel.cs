using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public struct PanelMessage
{
    public string header;
    public string description;
}

public class InstantPanel : MonoBehaviour
{
    public TMP_Text header;
    public TMP_Text description;
    public float hidePanelDelay=2.0f;

    public GameObject panel;

    private void Awake()
    {
        panel.SetActive(false);
    }

    void Start()
    {
        PuzzleBoard.instance.Onfailed += ShowPanel;
    }


    public void ShowPanel(string header, string description)
    {
        panel.SetActive(true);
        SetContent(header, description);
    }

    public void SetContent(string header, string description)
    {
        this.header.text = header;
        this.description.text = description;

        Invoke("HidePanel", hidePanelDelay);
    }

    void HidePanel()
    {
        panel.SetActive(false);
    }
}
