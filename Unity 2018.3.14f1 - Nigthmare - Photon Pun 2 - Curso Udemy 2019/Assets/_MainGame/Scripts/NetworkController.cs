using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Nigthmare {
    
    public class NetworkController : MonoBehaviourPunCallbacks {

        public Lobby lobbyScript;

        void Start() {

            
        }//Start

        void Update() {

            
        }//Update

        public override void OnConnected() {

            print("OnConnected");
        }

        public override void OnConnectedToMaster() {

            print("OnConnectedToMaster");
            lobbyScript.PanelLobbyActive();
        }

        public override void OnDisconnected(DisconnectCause cause) {

            print("OnDisconnected " + cause.ToString());
            lobbyScript.PanelLoginActive();
        }

        public void BtnCancelar() {

            PhotonNetwork.Disconnect(); //DisconnectByClientLogic
        }

        public void BtnLogin() {

            PhotonNetwork.ConnectUsingSettings();
        }

    }//SCRIPTNAME
    
}//namespace


