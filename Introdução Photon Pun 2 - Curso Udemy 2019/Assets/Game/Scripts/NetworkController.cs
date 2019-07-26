using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

using Photon.Pun.UtilityScripts;
using Hastable = ExitGames.Client.Photon.Hashtable;

namespace IntroducaoPhotonUdemy {

/* SCRIPT PARA CONEXÃO INICIAL DO PHOTON*/
#region NetworkController
    public class NetworkController : MonoBehaviourPunCallbacks, ILobbyCallbacks {

#region VARIABLES
          //bool isConnected = false;

        public GameObject loginUI;
        public GameObject partidasUI;

        public InputField inputPlayername, roomName;
        private string playerNameTemp;
        public GameObject myPlayer;

        Hastable gamemode = new Hastable();
        public byte gameMaxPlayer = 4;
        string gameModeKey = "gamemode";

 
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
            
            string[] typeGameRandom = new string[] {
                "PvP",
                "PvAI"
            };

            gamemode.Add(gameModeKey, typeGameRandom[Random.Range(0, typeGameRandom.Length)]);

            PhotonNetwork.JoinLobby(); //Entrar em um Lobby
        }

        public void ButtonCriarSala() {

            string tempRoomName = roomName.text;
            RoomOptions myRoomOptions = new RoomOptions() {MaxPlayers = 4}; //Opções que a sala terá
            PhotonNetwork.JoinOrCreateRoom(tempRoomName, myRoomOptions, TypedLobby.Default); //Entra na sala com o nome informado, se não houver, cria uma
        }

        public void ButtonPartidaPvP() {

            gamemode.Add(gameModeKey, "PvP");

/*          TypedLobby teste = new TypedLobby();
            teste.Name = "PvP";
            teste.Type = LobbyType.Default;
*/
            PhotonNetwork.JoinLobby();
        }

        public void ButtonPartidaPvAI() {

            gamemode.Add(gameModeKey, "PvAI");
            PhotonNetwork.JoinLobby();
        }

/* 
        public override void OnRoomListUpdate(List<RoomInfo> roomList) { //Atualização de salas existentes -Leva 5 segundos para ser atualizada

            ListaDeSalas(roomList);
        }

        void ListaDeSalas(List<RoomInfo> roomList) { 
            foreach (var item in roomList) {

                print("Room Name: " + item.Name);
                print("Room IsOpen: " + item.IsOpen);
                print("Room IsVisible: " + item.IsVisible);
                print("Room MaxPlayers: " + item.MaxPlayers);
                print("Room PlayerCount: " + item.PlayerCount);

                object temp;
                item.CustomProperties.TryGetValue(gameModeKey, out temp);

                print("Room CustomProperties: " + temp.ToString());
            }
        }
*/
 

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
            PhotonNetwork.JoinRandomRoom(gamemode, 0); //Tentar entrar em uma sala randomicamente
        }

        //se nao existir sala ao tentar entrar randomicamente, vai retornar erro
        //Será criada uma nova sala
        public override void OnJoinRandomFailed(short returnCode, string message) {

            string roomTemp = "Room" + Random.Range(1000, 10000);

            RoomOptions options = new RoomOptions();
            options.IsOpen = true;
            options.IsVisible = true;
            options.MaxPlayers = gameMaxPlayer;
            options.CustomRoomProperties = gamemode;
            options.CustomRoomPropertiesForLobby = new string[] { gameModeKey };

            PhotonNetwork.CreateRoom(roomTemp, options);
        }

        //Ao entrar em uma sala
        public override void OnJoinedRoom() {
            print("Jogador entrou em uma sala");
            print("Nome da sala: " + PhotonNetwork.CurrentRoom.Name); //Nome da sala
            print("Número de jogadores na sala: " + PhotonNetwork.CurrentRoom.PlayerCount); //quantidade de jogadores na sala

            loginUI.gameObject.SetActive(false);
            partidasUI.gameObject.SetActive(false);

            object typeGameValue;

            if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(gameModeKey, out typeGameValue)) {
                print("GameMode: " + typeGameValue.ToString());
                //print("GameMode: " + (string)typeGameValue);
            }

            foreach (var item in PhotonNetwork.PlayerList) { //Lista de informações do player ao entrar na sala

                print("Name: " + item.NickName);
                print("IsMaster? : " + item.IsMasterClient);

                //Customizando o player - Atribuindo valores aos mesmos
                Hastable playerCustom = new Hastable();
                playerCustom.Add("Lives", 3);
                playerCustom.Add("Score", 0);

                item.SetCustomProperties(playerCustom, null, null);

                item.SetScore(0);
            }

           //GameObject playerTemp = Instantiate(myPlayer, transform.position, transform.rotation) as GameObject;
           PhotonNetwork.Instantiate(myPlayer.name, transform.position, transform.rotation, 0); //Instanciando jogador no multiplayer
           //PhotonNetwork.LoadLevel(1);
        }

        //Chamado ao desconectar do servidor da photon (ex: internet caiu)
        public override void OnDisconnected(DisconnectCause cause) { 
            print("Desconetado do servidor: " + cause);
            //PhotonNetwork.ConnectToRegion("cn"); //Usado para conectar em um servidor de determinada região //As regiões estão em: https://doc.photonengine.com/en-us/realtime/current/connection-and-authentication/regions
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