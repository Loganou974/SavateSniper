using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SavateGame
{

    public class TutorialManager : MonoBehaviour
    {
        // Start is called before the first frame update
        XRController leftXRController, rightXRController;
        public GameObject okTuto1;

        public void Init(XRController _leftXRController, XRController _rightXRController)
        {
            leftXRController = _leftXRController;
            rightXRController = _rightXRController;

            leftXRController.GetComponent<XRRayInteractor>().selectEntered.AddListener(FirstGrab);
            rightXRController.GetComponent<XRRayInteractor>().selectEntered.AddListener(FirstGrab);
        }

        private void OnDisable()
        {
            leftXRController.GetComponent<XRRayInteractor>().selectEntered.RemoveListener(FirstGrab);
            rightXRController.GetComponent<XRRayInteractor>().selectEntered.RemoveListener(FirstGrab);
        }

        // Update is called once per frame
        void Update()
        {

            
         
        }

        public void FirstGrab(SelectEnterEventArgs args)
        {
            Debug.Log("ok");

            okTuto1.SetActive(true);


            leftXRController.GetComponent<XRRayInteractor>().selectEntered.RemoveListener(FirstGrab);
            rightXRController.GetComponent<XRRayInteractor>().selectEntered.RemoveListener(FirstGrab);
        }
    } 
}
