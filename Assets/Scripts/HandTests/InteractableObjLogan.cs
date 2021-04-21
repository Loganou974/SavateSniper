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
        public bool debug;

        Vector3 initPos;
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

         
        }

        private void Start()
        {
            initPos = transform.position;
        }

        private void Update()
        {
            currentPos = transform.position;
            distance = Vector3.Distance(initPos, currentPos);
         //   Debug.Log("Distance  : " +  distance);
            if (distance > yThreshold && !pressed)
            {
                PressButton();
                Debug.Log("ButtonPressed");

            }
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

        private void OnDrawGizmos()
        {
            if (debug)
            {
                Gizmos.DrawWireCube(initPos, new Vector3(0.01f, 0.01f, 0.01f));
                Gizmos.DrawWireCube(currentPos, new Vector3(0.01f, 0.01f, 0.01f));

                if (distance > yThreshold)
                {
                    Handles.Label(transform.position, "ok  "  + distance.ToString());

                }
                else
                    Handles.Label(transform.position, "not ok" + distance.ToString());


            }

        }
    }

}