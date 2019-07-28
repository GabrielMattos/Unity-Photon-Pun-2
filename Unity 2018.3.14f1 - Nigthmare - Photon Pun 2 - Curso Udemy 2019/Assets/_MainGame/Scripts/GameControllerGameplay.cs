using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Nigthmare {
    
    public class GameControllerGameplay : MonoBehaviourPunCallbacks {

        public GameObject myPlayer;
        public Transform[] spawnPlayer;

        void Start() {

            int tempPosition = Random.Range(0, spawnPlayer.Length);
            GameObject playerTemp = PhotonNetwork.Instantiate(myPlayer.name, spawnPlayer[tempPosition].position, spawnPlayer[tempPosition].rotation, 0) as GameObject;

                //Iniciando Countdownendgame
                    if(playerTemp.GetComponent<PhotonView>().Owner.IsMasterClient) {
                        ExitGames.Client.Photon.Hashtable myProps = new ExitGames.Client.Photon.Hashtable {
                        {CountdownEndGame.CountdownStartTime, (float)PhotonNetwork.Time}
                        }; 
                        PhotonNetwork.CurrentRoom.SetCustomProperties(myProps);
                    }

                    CheckPlayers();   
                }//Start

        void CheckPlayers() {

            if(PhotonNetwork.PlayerList.Length < 2) { //Se houver apenas 1 jogador na partida, o mesmo é vencedor
                foreach (var item in PhotonNetwork.PlayerList) {
                    print(item.NickName + " Vencedor!");
                }
            }
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) { //Se algum jogador sair da partida

            print(otherPlayer.NickName + " saiu da partida");
            CheckPlayers();
        }

    }//SCRIPTNAME
    
}//namespace


