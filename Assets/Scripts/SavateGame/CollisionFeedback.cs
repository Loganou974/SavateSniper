using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFeedback : MonoBehaviour
{
    public GameObject prefab; //obj to spawn on collision
    public Quaternion orientation;
    private float offset = 0.05f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            GiveFeedback(collision.GetContact(0).point);

        }
    }

    private void GiveFeedback(Vector3 pointPos)
    {
        orientation = transform.localRotation;
        GameObject newObj = Instantiate(prefab, pointPos, orientation) as GameObject;
        newObj.transform.localPosition += transform.up * offset;
        Destroy(newObj, 1);
    }
}
