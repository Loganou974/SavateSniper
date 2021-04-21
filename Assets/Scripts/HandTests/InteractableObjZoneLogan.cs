using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoganTools
{
    public class InteractableObjZoneLogan : MonoBehaviour
    {
        InteractableObjLogan interactableObj;
        public void Init(InteractableObjLogan _obj)
        {


            interactableObj = _obj;

        }

        private void OnCollisionEnter(Collision collision)
        {
        }

        private void OnCollisionExit(Collision collision)
        {
           
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("I detect");
            if (other.GetComponent<InteractableObjLogan>() != null)
            {
                Debug.Log("I detect interactable obj");
                if (other.GetComponent<InteractableObjLogan>()  == interactableObj)
                {
                    Debug.Log("I detect interact obj that is mine");
                    interactableObj.PressButton();
                }
                
            }
        }

      
    } 
}
