using UnityEngine;
using System.Collections.Generic;
using Oculus.Platform;
using Oculus.Platform.Models;
using System;
using TMPro;
using UnityEngine.UI;

namespace SavateGame
{

    public class OculusPlateformManager : MonoBehaviour
    {

        // my Application-scoped Oculus ID
        public ulong m_myID;
        // my Oculus user name
        public string playerUsername;

        public string playerImageURL;

        public URLImage urlImage;
        public TextMeshProUGUI usernameTxt;

        private void Awake()
        {
            try
            {
                Core.AsyncInitialize();
                Debug.Log("Plateform initializing...");
                Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCallBack);

            }
            catch (UnityException e)
            {
                Debug.LogError("Plateform failed to initialize");
                Debug.LogException(e);
                // UnityEngine.Application.Quit();
            }
        }

        private void EntitlementCallBack(Message msg)
        {
            if (msg.IsError)
            {
                Debug.LogError("You are not entitled to use this application");
                // UnityEngine.Application.Quit();
            }
            else
            {
                Debug.Log("Plateform initialized.");
                Debug.Log("You are entitled to use this application");
            }

            //now get the ID
            Users.GetLoggedInUser().OnComplete(GetLoggedInUserCallback);
        }

        private void GetLoggedInUserCallback(Message<User> msg)
        {
            if (msg.IsError)
            {
                // TerminateWithError(msg);
                return;
            }
            //msg.Data is all the Player infos

            m_myID = msg.Data.ID;
            playerUsername = msg.Data.OculusID;

            playerImageURL = msg.Data.ImageURL;
            urlImage.SetImage(playerImageURL);
            usernameTxt.text = playerUsername;
        }

    }

}