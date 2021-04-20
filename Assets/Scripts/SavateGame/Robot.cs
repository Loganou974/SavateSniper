using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SavateGame
{
    public class Robot : MonoBehaviour
    {
        public UnityEvent OnTargetHit;
        public Component[] allrbs;
  

        private void Awake()
        {
            allrbs = GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody joint in allrbs)
            {
                
                joint.gameObject.AddComponent<RobotTriggerZone>().Init(this);
            }


            RagDoll(false);
        }

        public void RagDoll(bool state)
        {
            foreach (Rigidbody joint in allrbs)
            {
                joint.isKinematic = !state;
               
            }
              
        }

    } 
}
