using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SavateGame
{
    public class RobotTriggerZone : MonoBehaviour
    {
       
        Robot robot;
        public  void Init(Robot _robot)
        {
            

            robot = _robot;
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                Debug.Log("It's a savate");
                robot.OnTargetHit.Invoke();
            }
        }
    } 
}
