using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nigthmare {
    
    public class Lobby : MonoBehaviour {

        public GameObject panelLogin;
        public GameObject panelLobby;

        public Text txtLobbyAguardar;
        public Text txtLobbyTimeStart;
        //public Text playerStatus;
        public Text txtPlayerStatus;
        public InputField inputPlayerName;
        public string playerName;

        public Text txtCountdown;

        public GameObject btnLobbyCancelar;

        //public string lobbyTimeStartText = "Start Game in {0}...";
        
        void Awake() {

            playerName = "Player" + Random.Range(1000, 10000);
        }

        void Start() {

            txtLobbyTimeStart.gameObject.SetActive(false);
            PanelLoginActive();

            inputPlayerName.text = playerName;

            txtPlayerStatus.gameObject.SetActive(false);

            btnLobbyCancelar.gameObject.SetActive(true);
        }//Start

        void Update() {

            //playerStatus.text = Photon.Pun.PhotonNetwork.NetworkClientState.ToString(); //Mostra o Status atual do player pelo servidor
            
        }//Update

        public void PanelLobbyActive() {

            panelLobby.gameObject.SetActive(true);
            panelLogin.gameObject.SetActive(false);
        }

        public void PanelLoginActive() {

            panelLobby.gameObject.SetActive(false);
            panelLogin.gameObject.SetActive(true);
        }

    }//Lobby
    
}//Nigthmare


