using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Animations.Rigging;

namespace SavateGame
{
    public class Savate : MonoBehaviour
    {
        public Color[] randomColors;
        private List<Color> colorPick;
        public GameObject rendererGO;
        XRGrabInteractable grabScript;
        public Transform rigTransform;
        DampedTransform[] dampedTransforms;
        Rigidbody rb;

        bool floppyMoveAlreadyActivated;

        public bool dropOnImpact = true;
        void Start()
        {
            grabScript = GetComponent<XRGrabInteractable>();
            rb = GetComponent<Rigidbody>();

            dampedTransforms = rigTransform.GetComponentsInChildren<DampedTransform>();

            RandomColor();
        }

       void FloppyMovement(bool state)
        {
            for (int i = 0; i < dampedTransforms.Length; i++)
            {
                dampedTransforms[i].data.maintainAim = state;
            }

            floppyMoveAlreadyActivated = state;
        }

        // Update is called once per frame
        void Update()
        {

            //if player grabs me and i'm not floppy activated
            if (rb.isKinematic && !floppyMoveAlreadyActivated)
            {
                //player grabs me
                FloppyMovement(true);
            }
            else if(!rb.isKinematic) FloppyMovement(false);
        }

        void RandomColor()
        {
            colorPick = new List<Color>();

            colorPick.AddRange(randomColors);

            Color color1 = colorPick[Random.Range(0, colorPick.Count)];
            colorPick.Remove(color1);
            rendererGO.GetComponent<Renderer>().materials[0].SetColor("_Color" ,color1);
            Color color2 = colorPick[Random.Range(0, colorPick.Count)];
            colorPick.Remove(color2);
            rendererGO.GetComponent<Renderer>().materials[1].SetColor("_Color", color2);

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (dropOnImpact && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}

