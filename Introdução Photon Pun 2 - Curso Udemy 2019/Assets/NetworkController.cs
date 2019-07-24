using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace IntroducaoPhotonUdemy {

    /* SCRIPT PARA CONEXÃO INICIAL DO PHOTON*/
    public class NetworkController : MonoBehaviourPunCallbacks {

        //bool isConnected = false;

        void Start() {

            ConectandoComServidorPhoton();
        }

        // Update is called once per frame
        void Update() {

            //TentandoConexaoComPhoton();
        }

       /*public void TentandoConexaoComPhoton() {

            if(!isConnected) {
                PhotonNetwork.ConnectUsingSettings(); //Conexão com photon
            }
        }*/

        public void ConectandoComServidorPhoton() {
            PhotonNetwork.ConnectUsingSettings(); //Conexão com photon
        } 

        //Chamado quando a conexao com photon é realizada
        public override void OnConnected() {
            print("Conectado com o Photon");
           // isConnected = true;
        }

         //Chamado assim que a conexão é realizada e validada
        public override void OnConnectedToMaster() {
            print("Conexão realizada e validada com sucesso");
            print("Server: " + PhotonNetwork.CloudRegion + " Ping: " + PhotonNetwork.GetPing()); //Local do servidor e ping
            PhotonNetwork.JoinLobby(); //Entrar em um Lobby
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
        }

        //Chamado ao desconectar do servidor da photon (ex: internet caiu)
        public override void OnDisconnected(DisconnectCause cause) { 
            print("Desconetado do servidor: " + cause);
            PhotonNetwork.ConnectToRegion("cn"); //Usado para conectar em um servidor de determinada região //As regiões estão em: https://doc.photonengine.com/en-us/realtime/current/connection-and-authentication/regions
           // isConnected = false;
        }
                                                
    }

}