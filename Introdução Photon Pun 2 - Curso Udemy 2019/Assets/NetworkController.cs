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

            PhotonNetwork.ConnectUsingSettings(); //Conexão com photon
            
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

        public override void OnConnected() {//Chamado quando a conexao com photon é realizada
            print("Conectado com o Photon");
           // isConnected = true;
        }

        public override void OnConnectedToMaster() { //Chamado assim que a conexão é realizada e validada
            print("Conexão realizada e validada com sucesso");
            print("Server: " + PhotonNetwork.CloudRegion + " Ping: " + PhotonNetwork.GetPing()); //Local do servidor e ping
        }

        public override void OnDisconnected(DisconnectCause cause) { //Chamado ao desconectar do servidor da photon (ex: internet caiu)
            print("Desconetado do servidor: " + cause);
            PhotonNetwork.ConnectToRegion("cn"); //Usado para conectar em um servidor de determinada região //As regiões estão em: https://doc.photonengine.com/en-us/realtime/current/connection-and-authentication/regions
           // isConnected = false;
        }
                                                
    }

}