using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using Photon.Pun.UtilityScripts;
using Hastable = ExitGames.Client.Photon.Hashtable;

namespace Nigthmare {
    
    public class NetworkController : MonoBehaviourPunCallbacks {

        public byte playersRoomMax = 2;

        public Lobby lobbyScript;

        public override void OnEnable() {

            base.OnEnable();
            CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimeIsExpired;
        }

        void Start() {

            PhotonNetwork.AutomaticallySyncScene = true; //Os players seguem o Master cliente nas cenas
        }//Start

        public override void OnDisable() {

            base.OnDisable();
            CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimeIsExpired;
        }

        public void OnCountdownTimeIsExpired() {

            StartGame();
        }

        void Update() {

            
        }//Update

        public override void OnConnected() {

            print("OnConnected");
        }

        public override void OnConnectedToMaster() {

            print("OnConnectedToMaster");
            lobbyScript.PanelLobbyActive();
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby() {
            
            Debug.Log("OnJoinedLobby");
            PhotonNetwork.JoinRandomRoom();
        }

        //Se não conseguir entrar em uma sala randomicamente, cria uma nova sala
        public override void OnJoinRandomFailed(short returnCode, string message) {

            Debug.Log("OnJoinRandomFailed");

            string roomName = "Room" + Random.Range(1000, 10000);
            RoomOptions myRoomOptions = new RoomOptions() {
                IsOpen = true,
                IsVisible = true,
                MaxPlayers = playersRoomMax
            };

            PhotonNetwork.CreateRoom(roomName, myRoomOptions, TypedLobby.Default);
        }

        public override void OnJoinedRoom() { //Master cliente entra primeiro, os proximos em seguida

            Debug.Log("OnJoinedRoom");

            PhotonNetwork.LocalPlayer.SetScore(0); //zerando a pontuação quando entra na sala
        }

        public override void OnPlayerEnteredRoom(Player newPlayer) { //quando um novo player entrar na sala

            Debug.Log("OnPlayerEnteredRoom");

            if(PhotonNetwork.CurrentRoom.PlayerCount == playersRoomMax) {

                foreach (var item in PhotonNetwork.PlayerList) {
                    if(item.IsMasterClient) {
                        //StartGame();
                        Hastable myProps = new Hastable {
                            {CountdownTimer.CountdownStartTime, (float)PhotonNetwork.Time}
                        };
                        
                        PhotonNetwork.CurrentRoom.SetCustomProperties(myProps);

                        PhotonNetwork.CurrentRoom.IsOpen = false;
                        PhotonNetwork.CurrentRoom.IsVisible = false;

                        return;
                    }
                }
            }
        }

        public override void OnRoomPropertiesUpdate(Hastable propertiesThatChanged) { //Se as propriedades da sala foram alteradas

            if(propertiesThatChanged.ContainsKey(CountdownTimer.CountdownStartTime)) {
                lobbyScript.txtLobbyTimeStart.gameObject.SetActive(true);
                lobbyScript.btnLobbyCancelar.gameObject.SetActive(false);
            }
        }

        void StartGame() { //A cena é carregada

            PhotonNetwork.LoadLevel("_Complete-Game-PVP");
        }

        public override void OnDisconnected(DisconnectCause cause) {

            print("OnDisconnected " + cause.ToString());
            lobbyScript.PanelLoginActive();
        }

        public void BtnCancelar() {

            PhotonNetwork.Disconnect(); //DisconnectByClientLogic
            lobbyScript.txtPlayerStatus.gameObject.SetActive(false);
        }

        public void BtnLogin() {

            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.NickName = lobbyScript.inputPlayerName.text;
            lobbyScript.txtPlayerStatus.gameObject.SetActive(true);
        }

    }//SCRIPTNAME
    
}//namespace


