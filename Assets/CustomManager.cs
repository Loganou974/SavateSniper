using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomManager : MonoBehaviour
{
    public Material[] skyboxes;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("ChangeSky")]
    public void ChangeSkybox()
    {
        if (i < skyboxes.Length - 1)
        {
            i++;
        }
        else
            i = 0;
        
        RenderSettings.skybox = skyboxes[i];
    }
}
