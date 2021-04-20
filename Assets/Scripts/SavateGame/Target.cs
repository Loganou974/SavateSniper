using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

namespace SavateGame
{
    [RequireComponent(typeof(AudioSource))]
    public class Target : TargetBtn
    {

        public GameObject pointFeedbackTxt;
        public int pointsToGive = 5;
        public float appearTime = 1;

        [Range(0.5f, 2f)]
        public float pitch = 1;

        AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.pitch = pitch;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            if (collision.gameObject.GetComponent<Rigidbody>() != null)
            {
              
                SavateGame._GameManager.Instance.Score += pointsToGive;
                CreateTextFeedback(collision);
            }
        }

        private void CreateTextFeedback(Collision collision)
        {
            GameObject newPointFeedbackTxt = Instantiate(pointFeedbackTxt, collision.contacts[0].point + new Vector3(0, 0, -1), pointFeedbackTxt.transform.rotation) as GameObject;
            newPointFeedbackTxt.GetComponent<TextMeshPro>().text = pointsToGive.ToString();
            Destroy(newPointFeedbackTxt, appearTime);
        }
    }
}
