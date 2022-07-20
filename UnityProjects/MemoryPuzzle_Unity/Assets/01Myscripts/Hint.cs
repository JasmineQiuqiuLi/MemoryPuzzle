using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hint : MonoBehaviour
{

    bool showHint = false;
    

    public void ShowHint()
    {
        showHint = !showHint;
        HintCanvas.instance.SetCanvasStatus(showHint);
    }
}
