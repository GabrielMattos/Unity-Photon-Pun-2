using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;


namespace SelectPlayerPhoton {
    
    public class SelectPlayer : MonoBehaviour {

        public int playerSelected = 0;
        public GameObject playerBody;

        public Image playerIconCanvas;

        public GameObject playerCanvas;

        PhotonView myPhotonView;

        void Start() {

            

            myPhotonView = GetComponent<PhotonView>();

            if(!myPhotonView.IsMine) {
                playerCanvas.gameObject.SetActive(false);
            }

            SwitchPlayer();
        }//Start

        void Update() {

            
        }//Update

        void SwitchPlayer() {

            int i = 0;

            foreach (Transform item in playerBody.transform) {
                if(i == playerSelected) {
                    item.gameObject.SetActive(true);
                    if(item.gameObject.GetComponent<PlayerConfig>()) {
                        playerIconCanvas.sprite = item.gameObject.GetComponent<PlayerConfig>().playerData.playerIcon;

                        //PlayerData playerdataTemp = item.gameObject.GetComponent<PlayerConfig>().playerData;
                        //print("Name: " + playerdataTemp.playerName);
                        //print("Name: " + playerdataTemp.playerStatus);
                        //print("Name: " + playerdataTemp.playerType);
                        //print("Name: " + playerdataTemp.playerSpeed);
                    }

                } else {
                    item.gameObject.SetActive(false);
                }

                i++;
            }
        }

        public void BtnBack() {

            playerSelected--;

            if(playerSelected < 0) {
                playerSelected = playerBody.transform.childCount - 1;
            }

            SwitchPlayer();
        }

        public void BtnForward() {

            playerSelected++;

            if(playerSelected > (playerBody.transform.childCount - 1)) {
                playerSelected = 0;
            }

            SwitchPlayer();
        }

    }//SCRIPTNAME
    
}//namespace


