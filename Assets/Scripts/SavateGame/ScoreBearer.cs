using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace SavateGame
{

    /// <summary>
    /// Displays the score to the player. Look at player too !
    /// </summary>
    /// 
    public class ScoreBearer : MonoBehaviour
    {
        public Transform playerTrans;
        public TextMeshPro txtFeedback;
        // Start is called before the first frame update
        void Start()
        {
            playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
           
            txtFeedback.text = SavateGame._GameManager.Instance.Score.ToString("0000");
        }


    }
}

