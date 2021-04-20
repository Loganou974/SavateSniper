using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SavateGame
{
    public class TargetBtn : MonoBehaviour
    {
        public UnityEvent OnTargetHit;


        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Rigidbody>() != null)
            {
                OnTargetHit.Invoke();
                
            }
        }

        protected void DelayedHide(float delay)
        {
            StartCoroutine(DelayingAndHiding(delay));
        }

        protected IEnumerator DelayingAndHiding(float delay)
        {
            yield return new WaitForSeconds(delay);

            gameObject.SetActive(false);
        }
    }
}
