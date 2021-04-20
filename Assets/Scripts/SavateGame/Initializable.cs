using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializable : MonoBehaviour
{
    public bool initialized = false;

    public virtual void Init(bool state = true)
    {
        initialized = state;
    }
}
