using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using Oculus.Platform;
using Oculus.Platform.Models;
using System;
using TMPro;

// Coordinates updating leaderboard scores and polling for leaderboard updates.
namespace SavateGame
{
    /// <summary>
    /// Get the Oculus platform leaderboard, submit GameManager's score to it.
    /// </summary>
    public class LeaderboardManager : MonoBehaviour
    { 
        // API NAME for the leaderboard 
        private const string leaderboardName = "SavateSniperLeaderboard";

        List<LeaderboardEntry> lbe;
        public int amount = 5;

        public TextMeshPro txtFeedback;

        OculusPlateformManager opManager;

        public SpriteRenderer internetConnectionStatutSprite;

        public bool checkInternet;

        bool isConnected;

        private void Awake()
        {
            opManager = GetComponent<OculusPlateformManager>();
         if(checkInternet)   InvokeRepeating("CheckInternetConnection", 0, 10);

        }


        void CheckInternetConnection()
        {
            StartCoroutine(CheckingInternetConnection((isConnected) => {/* handle connection status here */}));
        }
        public IEnumerator CheckingInternetConnection(Action<bool> syncResult)
        {
            internetConnectionStatutSprite.color = Color.blue;

            const string echoServer = "http://google.com";

            bool result;
            using (var request = UnityEngine.Networking.UnityWebRequest.Head(echoServer))
            {
                request.timeout = 5;
                yield return request.SendWebRequest();
                result = !(request.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError) && !(request.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError) && request.responseCode == 200;
               
            }


            if (result)
            {
                internetConnectionStatutSprite.color = Color.green;
            }
            else
                internetConnectionStatutSprite.color = Color.red;

            syncResult(result);


           
        }

        public void SubmitMatchScores()
        {
           
            if (_GameManager.Instance.Score > 0)
            {


                Leaderboards.WriteEntry(leaderboardName, _GameManager.Instance.Score).OnComplete(ScoreSubmitted);

                Debug.Log("Saving Score : " + _GameManager.Instance.Score);
            }
            else { GetLeaderboardData(); }
        }

        private void ScoreSubmitted(Message<bool> msg)
        {
            if (!msg.IsError)
            {
                Debug.Log("Score submitted");
            }
            else
            {
                Debug.LogError("Score failed to be submitted");
            }

            GetLeaderboardData();
        }

        public void GetLeaderboardData()
        {
            Debug.Log("Getting Leaderboard data");
            txtFeedback.text = "Fetching Leaderboard...";
            lbe = new List<LeaderboardEntry>(); 

            Leaderboards.GetEntries(leaderboardName, amount, LeaderboardFilterType.None, LeaderboardStartAt.Top).OnComplete(LeaderboardGetCallback);

        }

        private void LeaderboardGetCallback(Message<LeaderboardEntryList> msg)
        {
            if (!msg.IsError)
            {
                var entries = msg.Data;
               
                foreach (var entry in entries)
                {
                    lbe.Add(entry);
                    Debug.Log(entry.User.DisplayName + "  " + entry.Score);
                }

                Debug.Log("Leaderboards fetched successfully");
                UpdateUI();
            }
            else
            {
                Debug.LogError("Error getting the leaderboards");
                Debug.LogError(msg.GetError().Message);
                txtFeedback.text = "Error getting the leaderboards";


                //Let's retry shall we ?
                StartCoroutine(WaitAndRetry());
            }
        }


        /// <summary>
        /// Wait X seconds and retry leaderboard connection
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitAndRetry()
        {
            float secs = 5;
            txtFeedback.text = "Error getting the leaderboards. Retrying in 5 seconds...";
           yield return new WaitForSeconds(secs);
            GetLeaderboardData();
        }

        private void UpdateUI()
        {
            txtFeedback.text = "";

            foreach (LeaderboardEntry entry in lbe)
            {
                Debug.Log("comparing " + entry.User.ID.ToString() + "and " + opManager.m_myID);
               if(entry.User.ID == opManager.m_myID)
                {
                    Debug.Log("User detected in leaderboard !");
                    txtFeedback.text += "<color=#FFFB00>" + "Rank : " + entry.Rank + "           User : " + entry.User.DisplayName + "       Score :  " + entry.Score + "</color>"+  "\n";
                }
                else
                {
                    Debug.Log("Another user");
                    txtFeedback.text += "Rank : " + entry.Rank + "           User : " + entry.User.DisplayName + "       Score :  " + entry.Score + "\n";
                }
               
            }
        }
    } 
}