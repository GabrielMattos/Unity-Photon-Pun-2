using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Nigthmare {
    
    public class NameMultiplayer : MonoBehaviour {

        private PhotonView myPhotonView;

        public Text txtPlayerName;

        public GameObject playerCanvas;

        void Start() {

            myPhotonView = GetComponent<PhotonView>();

            txtPlayerName.text = myPhotonView.Owner.NickName;
            
        }//Start

        void Update() {

            playerCanvas.transform.LookAt(Camera.main.transform);            
        }//Update

    }//SCRIPTNAME
    
}//namespace


