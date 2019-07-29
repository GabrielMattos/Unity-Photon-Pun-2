﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountdownTimer.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Utilities,
// </copyright>
// <summary>
// This is a basic CountdownTimer. In order to start the timer, the MasterClient can add a certain entry to the Custom Room Properties,
// which contains the property's name 'StartTime' and the actual start time describing the moment, the timer has been started.
// To have a synchronized timer, the best practice is to use PhotonNetwork.Time.
// In order to subscribe to the CountdownTimerHasExpired event you can call CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
// from Unity's OnEnable function for example. For unsubscribing simply call CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;.
// You can do this from Unity's OnDisable function for example.
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

using ExitGames.Client.Photon;
using Photon.Pun;

    /// <summary>
    /// This is a basic CountdownTimer. In order to start the timer, the MasterClient can add a certain entry to the Custom Room Properties,
    /// which contains the property's name 'StartTime' and the actual start time describing the moment, the timer has been started.
    /// To have a synchronized timer, the best practice is to use PhotonNetwork.Time.
    /// In order to subscribe to the CountdownTimerHasExpired event you can call CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
    /// from Unity's OnEnable function for example. For unsubscribing simply call CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;.
    /// You can do this from Unity's OnDisable function for example.
    /// </summary>
    public class CountdownEndGame : MonoBehaviourPunCallbacks
    {
        public const string CountdownStartTime = "StartTimeEnd";

        /// <summary>
        /// OnCountdownTimerHasExpired delegate.
        /// </summary>
        //public delegate void CountdownTimerHasExpired();

        /// <summary>
        /// Called when the timer has expired.
        /// </summary>
        //public static event CountdownTimerHasExpired OnCountdownTimerHasExpired;

        private bool isTimerRunning;

        private float startTime;

        [Header("Reference to a Text component for visualizing the countdown")]
        public Text Text;

        [Header("Countdown time in seconds")]
        public float Countdown = 60.0f;

        public void Start()
        {
            if (Text == null)
            {
                Debug.LogError("Reference to 'Text' is not set. Please set a valid reference.", this);
                return;
            }
        }

        public void Update()
        {
            if (!isTimerRunning)
            {
                return;
            }

            float timer = (float)PhotonNetwork.Time - startTime;
            float countdown = Countdown - timer;
            string minutes = ((int)countdown / 60).ToString();
            string seconds = ((int)countdown % 60).ToString("00");

            if(seconds.Equals("60")) {
                return;
            }
            
            string timeInText = minutes + ":" + seconds;
            Text.text = timeInText;

            if(timeInText.Equals("0:00")) {
                print("FIM DE JOGO");
                isTimerRunning = false;
                this.GetComponent<Nigthmare.GameControllerGameplay>().GameOver();
            }

            /* Text.text = string.Format("Game starts in {0} seconds", countdown.ToString("n2"));

            if (countdown > 0.0f)
            {
                return;
            }

            isTimerRunning = false;

            Text.text = string.Empty;

            if (OnCountdownTimerHasExpired != null)
            {
                OnCountdownTimerHasExpired();
            }*/
            
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {

            Debug.Log("Passei por aqui!");

            object startTimeFromProps;

            if (propertiesThatChanged.TryGetValue(CountdownStartTime, out startTimeFromProps))
            {
                isTimerRunning = true;
                startTime = (float)startTimeFromProps;
            }

            object isGameOver;

             if (propertiesThatChanged.TryGetValue("isGameOver", out isGameOver))
            {
               if((bool)isGameOver) {
                   isTimerRunning = false;
               }
            }
        }
    }