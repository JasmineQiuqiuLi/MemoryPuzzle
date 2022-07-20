using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frames : MonoBehaviour
{
    //if the bool is set static, newly creately frames will not set to the children of the Frames empty object
    bool created = false;
    public static Frames instance;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        instance = this;
    }

    public void SetFramesAsParent(GameObject obj)
    {
        obj.transform.parent = transform;
    }
}
