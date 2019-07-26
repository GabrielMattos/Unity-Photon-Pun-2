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

        public string lobbyTimeStartText = "Start Game in {0}...";

        void Start() {

            txtLobbyTimeStart.gameObject.SetActive(false);
            PanelLoginActive();
        }//Start

        void Update() {

            
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


