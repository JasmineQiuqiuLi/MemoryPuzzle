using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPictures : MonoBehaviour
{
    bool _created=false;
    private void Awake()
    {
        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;
        }
    }
}
