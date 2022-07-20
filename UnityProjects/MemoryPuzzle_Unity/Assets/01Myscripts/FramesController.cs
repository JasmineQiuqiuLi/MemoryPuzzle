using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramesController : MonoBehaviour
{
    public static FramesController instance;

    public GameObject[] frames;
    private void Awake()
    {
        instance = this;

        //Frames will not be destroyed due to DontDestroyOnLoad, so all the frames copy need to be included.
        frames = GameObject.FindGameObjectsWithTag("Frames");
    }
    public void SetFramesStatus(bool status)
    {
        foreach(var frame in frames)
        {
            frame.SetActive(status);
        }
    }
}
