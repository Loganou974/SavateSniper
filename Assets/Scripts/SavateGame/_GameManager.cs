using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.XR.Interaction.Toolkit;

namespace SavateGame
{

    /// <summary>
    /// Handles all the core loop rules from gameflow to score management
    /// </summary>
    public class _GameManager : MonoBehaviour
    {
        public static _GameManager Instance;

        public XRController leftXRController, rightXRController;

        public Timer timer;

        public float timeMax = 45;

        public GameObject[] hiddenObjectsOnInit;

        public GameObject replayBtn;

        public GameObject playBtn;

        public Target[] targets;

        public Image fadeImage;

        public UnityEvent OnGameStart, OnGameOver;

        LeaderboardManager leaderboardManager;
        TutorialManager tutorialManager;

        private void OnEnable()
        {
            //SINGLETON
            if (Instance == null) Instance = this;
            else Destroy(this);

            targets = FindObjectsOfType<Target>();
            leaderboardManager = GetComponent<LeaderboardManager>();
            tutorialManager = GetComponent<TutorialManager>();

           SearchForControllers();

            //Initialisation
            Init();

            //Init Tuto
            tutorialManager.Init(leftXRController, rightXRController);
        }

        private void SearchForControllers()
        {
           
            StartCoroutine(SearchingControllers());


           
        }

        IEnumerator SearchingControllers()
        {
            XRController[] controllers = FindObjectsOfType<XRController>();

            while(controllers.Length <= 0)
            {
                yield return null;
            }

            if (controllers[0].controllerNode == UnityEngine.XR.XRNode.LeftHand)
            {
                leftXRController = controllers[0];
                rightXRController = controllers[1];

            }
            else
            {
                leftXRController = controllers[1];
                rightXRController = controllers[0];
            }
        }

        private int score;
        public int Score { get { return score; } set { score = value; } }


        void Init()
        {

            //Set timer
            timer.maxTime = timeMax;

            //Hide things
            for (int i = 0; i < hiddenObjectsOnInit.Length; i++)
            {
                hiddenObjectsOnInit[i].SetActive(false);
            }
           

            //Hide Targets
            ShowHideTargets(false);
        }

        public void StartGame(float delay)
        {

            StartCoroutine(WaitAndStartGame(delay));
        }

        IEnumerator WaitAndStartGame(float delay)
        {
          

            yield return new WaitForSeconds(delay);


            OnGameStart.Invoke();
        }

        public void GameOver()
        {
            OnGameOver.Invoke();

            StartCoroutine(WaitAndGameOver());
        }

        IEnumerator WaitAndGameOver()
        {
            ShowHideTargets(false);

            yield return new WaitForSeconds(2);
         
            
            //Show ReplayBtn

            replayBtn.SetActive(true);
            leaderboardManager.SubmitMatchScores();
        }

        public void RestartLevel()
        {
            StartCoroutine(RestartingLevel());
        }

        IEnumerator RestartingLevel()
        {
            FadeManager.Instance.FadeOut();
          //  Oculus.Platform.CallbackRunner callbackRunner = FindObjectOfType<Oculus.Platform.CallbackRunner>();
           // VRDebug.VR_DebugManager vr_debug = FindObjectOfType<VRDebug.VR_DebugManager>();
            //Destroy(callbackRunner.gameObject);
            //Destroy(vr_debug.gameObject);
            yield return new WaitForSeconds(3);

         /*   for (int i = 0; i < SceneManager.sceneCount; i++)
            {
               Debug.Log(SceneManager.GetSceneAt(i).name);
            } */

            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }

        public void ShowHideTargets(bool state)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].transform.parent == null) targets[i].gameObject.SetActive(state);
            }
        }

        public void DropAllSavates()
        {
            Savate[] savates = FindObjectsOfType<Savate>();

            for (int i = 0;  i < savates.Length; i++)
            {
                Rigidbody rb = savates[i].GetComponent<Rigidbody>();
                if (rb.velocity.magnitude != 0  && rb.useGravity == false )
                {
                    rb.velocity = Vector3.zero;
                    rb.useGravity = true;
                }
            }
        }
    }

   
}

