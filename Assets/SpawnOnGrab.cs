using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnGrab : MonoBehaviour
{
    public GameObject source;
    public bool autoDestroy = true;
    public float autoDestroyTime = 2;

    public Transform spawnTransform;
    public Transform parent;


    protected virtual void Awake()
    {
        if (spawnTransform == null) spawnTransform = transform;

    }
    public virtual void Spawn()
    {
        Debug.Log("Spawning");
        GameObject newObj = Instantiate(source, spawnTransform.position, spawnTransform.rotation) as GameObject;

        newObj.transform.SetParent(parent);
        if (autoDestroy) Destroy(newObj, autoDestroyTime);
    }
}
