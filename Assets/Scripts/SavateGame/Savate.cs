using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SavateGame
{
    public class Savate : MonoBehaviour
    {
        public Color[] randomColors;
        private List<Color> colorPick;
        public GameObject rendererGO;
        XRGrabInteractable grabScript;

        public bool dropOnImpact = true;
        void Start()
        {
            grabScript = GetComponent<XRGrabInteractable>();
            RandomColor();
        }

        // Update is called once per frame
        void Update()
        {
          
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

