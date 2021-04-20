using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OculusSampleFramework;
using System;

public class HandTrackingGrabber : OVRGrabber
{
    private OVRHand hand;
    public float pinchTreshold = 0.7f;
    public float pushForce = 5;

    public  bool indexIsPinching;
    public bool thumbIsPinching;

    public UnityEvent OnIndexBeginPinch, OnIndexEndPinch, OnThumbBeginPinch, OnThumbEndPinch;

    private void OnEnable()
    {
        OnIndexBeginPinch.AddListener(GrabBegin);
        OnIndexEndPinch.AddListener(GrabEnd);

        OnThumbBeginPinch.AddListener(GrabBegin);
        OnThumbEndPinch.AddListener(GrabEnd);


    }
    private void OnDisable()
    {
        OnIndexBeginPinch.RemoveListener(GrabBegin);
        OnIndexEndPinch.RemoveListener(GrabEnd);

        OnThumbBeginPinch.RemoveListener(GrabBegin);
        OnThumbEndPinch.RemoveListener(GrabEnd);
    }

    protected override void Start()
    {
        base.Start();
        hand = GetComponent<OVRHand>();
    }


    public  void FixedUpdate()
    {
        CheckIndexPinch();

        CheckThumbPinch();
    }

    private void CheckThumbPinch()
    {
         float pinchStrength = GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Index);
       // thumbIsPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Thumb);

        thumbIsPinching = pinchStrength > pinchTreshold;

        if (!m_grabbedObj && thumbIsPinching && m_grabCandidates.Count > 0) ThumbBeginPinch();
        else if (m_grabbedObj && !thumbIsPinching) ThumbEndPinch();
    }

    private void CheckIndexPinch()
    {
        float pinchStrength = GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Index);
        // indexIsPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        indexIsPinching = pinchStrength > pinchTreshold;

        if (!m_grabbedObj && indexIsPinching && m_grabCandidates.Count > 0 ) IndexBeginPinch();
        else if(m_grabbedObj && !indexIsPinching) IndexEndPinch();
    }

    void IndexBeginPinch()
    {
        OnIndexBeginPinch.Invoke();
    }

    void IndexEndPinch()
    {
        OnIndexEndPinch.Invoke();
    }

    void ThumbBeginPinch()
    {
        OnThumbBeginPinch.Invoke();
    }
    void ThumbEndPinch()
    {
        OnThumbEndPinch.Invoke();
    }

    protected override void GrabEnd()
    {
        if (m_grabbedObj)
        {
            Vector3 linearVelocity = (transform.position - m_lastPos)/Time.fixedDeltaTime;
            Vector3 angularVelocity = (transform.eulerAngles - m_lastRot.eulerAngles) / Time.fixedDeltaTime;

            GrabbableRelease(linearVelocity * pushForce, angularVelocity * pushForce);
        }

        GrabVolumeEnable(true);
    }
}
