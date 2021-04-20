using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SavateGame
{
    public class Timer : Initializable
    {
        public float curTime;
        [HideInInspector] public float maxTime;
        TextMeshPro txtFB;
        public int waitTimeBeforeGameOver = 2;

        private void Awake()
        {
            txtFB = GetComponent<TextMeshPro>();

            curTime = maxTime;
        }



        private void Update()
        {
            if (initialized)
            {
                if (curTime > 0)
                {
                    Count();
                }
                else
                {
                    SavateGame._GameManager.Instance.GameOver();
                }
            }

        }

        void Count()
        {
            curTime -= Time.deltaTime;
            txtFB.text = curTime.ToString("00");
        }


      


    }

   
}
