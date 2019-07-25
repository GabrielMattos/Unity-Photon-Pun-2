using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace IntroducaoPhotonUdemy {

/* SCRIPT PARA CONEXÃO INICIAL DO PHOTON*/
#region NetworkController
    public class NetworkController : MonoBehaviourPunCallbacks {

#region VARIABLES
          //bool isConnected = false;

        public GameObject loginUI;
        public GameObject partidasUI;

        public InputField inputPlayername, roomName;
        private string playerNameTemp;
        public GameObject myPlayer;
 
#endregion 
#region UNITY
        void Start() {
            //ConectandoComServidorPhoton();
            AleatoryNickName();
            AleatoryRoomName();
            loginUI.gameObject.SetActive(true);
            
        }

        // Update is called once per frame
        void Update() {
            //TentandoConexaoComPhoton();
        }
#endregion
#region FUNÇÕES
        void AleatoryNickName() {

            playerNameTemp = "Player" + Random.Range(1000, 10000);
            inputPlayername.text = playerNameTemp;
        }

        void AleatoryRoomName() {

            roomName.text = "Room" + Random.Range(1000, 10000);
        }

        public void Login() {

            if(inputPlayername.text != "") {
                PhotonNetwork.NickName = inputPlayername.text; //Insere o nick do player no Photon
            } else {
                PhotonNetwork.NickName = playerNameTemp;
            }  
            ConectandoComServidorPhoton();
            loginUI.gameObject.SetActive(false);
        }

        public void ButtonBuscarPartidaRapida() {

            PhotonNetwork.JoinLobby(); //Entrar em um Lobby
        }

        public void ButtonCriarSala() {

            string tempRoomName = roomName.text;
            RoomOptions myRoomOptions = new RoomOptions() {MaxPlayers = 1}; //Opções que a sala terá
            PhotonNetwork.JoinOrCreateRoom(tempRoomName, myRoomOptions, TypedLobby.Default); //Entra na sala com o nome informado, se não houver, cria uma
        }

        public void ConectandoComServidorPhoton() {

            PhotonNetwork.ConnectUsingSettings(); //Conexão com photon
        }

#endregion
#region PHOTON_CALLBACKS
    //Chamado quando a conexao com photon é realizada
        public override void OnConnected() {
            print("Conectado com o Photon");
           // isConnected = true;
        }

         //Chamado assim que a conexão é realizada e validada
        public override void OnConnectedToMaster() {
            print("Conexão realizada e validada com sucesso");
            print("Server: " + PhotonNetwork.CloudRegion + " Ping: " + PhotonNetwork.GetPing()); //Local do servidor e ping
            partidasUI.gameObject.SetActive(true);
            //PhotonNetwork.JoinLobby(); //Entrar em um Lobby
        }

        //Ao entrar no Lobby
        public override void OnJoinedLobby() {
            print("Entrou no Lobby");
            PhotonNetwork.JoinRandomRoom(); //Tentar entrar em uma sala randomicamente
        }

        //se nao existir sala ao tentar entrar randomicamente, vai retornar erro
        //Será criada uma nova sala
        public override void OnJoinRandomFailed(short returnCode, string message) {
            PhotonNetwork.CreateRoom(null);
        }

        //Ao entrar em uma sala
        public override void OnJoinedRoom() {
            print("Jogador entrou em uma sala");
            print("Nome da sala: " + PhotonNetwork.CurrentRoom.Name); //Nome da sala
            print("Número de jogadores na sala: " + PhotonNetwork.CurrentRoom.PlayerCount); //quantidade de jogadores na sala

            loginUI.gameObject.SetActive(false);
            partidasUI.gameObject.SetActive(false);

           //GameObject playerTemp = Instantiate(myPlayer, transform.position, transform.rotation) as GameObject;
           PhotonNetwork.Instantiate(myPlayer.name, transform.position, transform.rotation, 0); //Instanciando jogador no multiplayer
        }

        //Chamado ao desconectar do servidor da photon (ex: internet caiu)
        public override void OnDisconnected(DisconnectCause cause) { 
            print("Desconetado do servidor: " + cause);
            PhotonNetwork.ConnectToRegion("cn"); //Usado para conectar em um servidor de determinada região //As regiões estão em: https://doc.photonengine.com/en-us/realtime/current/connection-and-authentication/regions
           // isConnected = false;
        }
#endregion
#region COMMENTS
           /*public void TentandoConexaoComPhoton() {

            if(!isConnected) {
                PhotonNetwork.ConnectUsingSettings(); //Conexão com photon
            }
        }*/
#endregion
                                                
    }
#endregion

}