using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRGrabberLogan : OVRGrabber
{

    Vector3 linearVelocity;
    Vector3 angularVelocity;
    public float linearPushForce = 2;
    public float angularPushForce = 1;

    private void FixedUpdate()
    {
        linearVelocity = (transform.position - m_lastPos) / Time.fixedDeltaTime;
        angularVelocity = (transform.eulerAngles - m_lastRot.eulerAngles) / Time.fixedDeltaTime;
    }
    protected override void GrabEnd()
    {
        if (m_grabbedObj)
        {
            GrabbableRelease(linearVelocity * linearPushForce, angularVelocity * angularPushForce);
        }

        GrabVolumeEnable(true);
    }
}
