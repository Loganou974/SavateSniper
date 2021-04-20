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

            if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                Debug.Log("BeginHover");
                interactableObj.OnBeginHover.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                Debug.Log("EndHover");
                interactableObj.OnEndHover.Invoke();
            }
        }
    } 
}
