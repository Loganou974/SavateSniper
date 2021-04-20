using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OculusSampleFramework;
using System;

public class ButtonListener : MonoBehaviour
{
    public UnityEvent proximityEvent;
    public UnityEvent contactEvent;
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;

    private void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    public void InitiateEvent(InteractableStateArgs state)
    {
        Debug.Log("detected");
        if (state.NewInteractableState == InteractableState.ProximityState)
            proximityEvent.Invoke();
        else if (state.NewInteractableState == InteractableState.ContactState)
            contactEvent.Invoke();
        else if (state.NewInteractableState == InteractableState.ActionState)
            actionEvent.Invoke();
        else
            defaultEvent.Invoke();
    }
}
