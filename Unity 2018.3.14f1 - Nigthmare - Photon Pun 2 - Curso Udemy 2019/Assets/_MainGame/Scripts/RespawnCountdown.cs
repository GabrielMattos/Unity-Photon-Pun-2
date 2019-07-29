using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace CompleteProject {
    
    public class RespawnCountdown : MonoBehaviour {

        float countdown = 5f;
        float startTime;

        public string respawnMessage = "Respawn em";
        public Text txtRespawn;
        
        void Start() {

            startTime = (float)PhotonNetwork.Time;
            StartCoroutine(WaitForDestroy(countdown));
            
        }//Start

        IEnumerator WaitForDestroy(float time) {

            yield return new WaitForSeconds(time);
            Destroy(this.gameObject);
        }

        void Update() {

            float timer = (float)PhotonNetwork.Time - startTime;
            float countdownTemp = countdown - timer;
            string seconds = (countdownTemp % 60).ToString("0");

            if(countdownTemp < 0.0f) {
                return;
            }

            txtRespawn.text = respawnMessage + "\n" + seconds;
                
        }//Update

    }//SCRIPTNAME
    
}//namespace


