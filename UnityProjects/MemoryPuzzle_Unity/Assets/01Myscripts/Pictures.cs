using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pictures : MonoBehaviour
{
    public static bool _created=false;
    private void Awake()
    {

        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;
        }
    }

}
