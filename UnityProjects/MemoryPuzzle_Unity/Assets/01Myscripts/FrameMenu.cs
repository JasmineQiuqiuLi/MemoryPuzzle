using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameMenu : MonoBehaviour
{
    public GameObject frameMenuPrefab;

    public void HandleClick(Transform transform,Vector3 pos)
    {
        //the click event may not occur very frequently, so instantiate will not consume a lot of memory.
        //if this happens frequently, I'll iniate and deactivate it at the awake state, and activate it whenever needed.
        GameObject frameMenu=Instantiate(frameMenuPrefab,pos,frameMenuPrefab.transform.rotation);
        frameMenu.transform.parent = transform;
    }

    public void DestroyFrameMenu()
    {
        Destroy(gameObject);
    }


}
