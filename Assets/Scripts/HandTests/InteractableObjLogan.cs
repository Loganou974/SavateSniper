using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace LoganTools
{
    public class InteractableObjLogan : MonoBehaviour
    {
        //public Collider hoverTrigger;
        public InteractableObjZoneLogan activateZone;
        public bool debug;

        Vector3 initPos;
        Vector2 contactPos;
        Vector3 currentPos;
        public float yThreshold = 0.07f;
        float distance;

        bool pressed;

        Material defaultMat;
        Rigidbody rb;
        AudioSource audioSource;
        public GameObject particleFB;
        private XRController xr;



        public UnityEvent OnBeginHover, OnEndHover, OnBeginContact, OnEndContact, OnButtonPressed;

      

        private void Awake()
        {
            // hoverTrigger.gameObject.AddComponent<InteractableObjZoneLogan>().Init(this);
            rb = GetComponent<Rigidbody>();
            defaultMat = GetComponent<MeshRenderer>().material;
            audioSource = GetComponent<AudioSource>();

            //hide particles
            particleFB.SetActive(false);


            if (activateZone != null) activateZone.Init(this);
            else Debug.LogWarning("You should set the ActivateZone GO, Button wont trigger OnButtonPressed without it.");
        }

        private void Start()
        {
            initPos = transform.position;
          //  contactPos = initPos;
        }

      

        private void OnCollisionEnter(Collision collision)
        {
         
            OnBeginContact.Invoke();
            
        }

        private void OnCollisionExit(Collision collision)
        {
           
            OnEndContact.Invoke();
        }

       

        public void PressButton()
        {
            if (!pressed)
            {
                PressAndRelease();
                OnButtonPressed.Invoke();
                audioSource.Play();
            }
          

        }

        public void PressAndRelease()
        {
            StartCoroutine(PressingAndReleasing());
        }

        IEnumerator PressingAndReleasing()
        {
            Debug.Log("button pressed");
            //Activate particle
            particleFB.SetActive(true);

            //activate haptics
            ActivateHaptic(xr);

            pressed = true;
            rb.isKinematic = true;

            //desact collider
            GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(2f);

            //Reset pos
            transform.position = initPos;

            //reactivate all
            rb.isKinematic = false;
            GetComponent<MeshRenderer>().material = defaultMat;
            GetComponent<Collider>().enabled = true;
            particleFB.SetActive(false);
            pressed = false;
        }

      

        void ActivateHaptic(XRController _xr)
        {
           if(_xr !=null) _xr.SendHapticImpulse(0.7f, 0.5f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("GameController"))
            {
                xr = other.gameObject.GetComponent<XRController>();
               
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(xr != null)
            {
                if (other.gameObject == xr.gameObject)
                {
                    xr = null;
                }
            }
           
        }

     
    }

}