using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{

    public GameObject prefab;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create()
    {
      GameObject newObj =   Instantiate(prefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
    }
}
