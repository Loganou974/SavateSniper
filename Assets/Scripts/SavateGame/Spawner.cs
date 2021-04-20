using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public bool autoDestroy = true;
    public float autoDestroyTime = 2;

    public void Spawn()
    {
        GameObject newObj = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;

        if (autoDestroy) Destroy(newObj, autoDestroyTime);
    }
}
