using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts; //Para pegar score

namespace Nigthmare {
    
    public class GameControllerGameplay : MonoBehaviourPunCallbacks {

        public GameObject myPlayer;
        public Transform[] spawnPlayer;

        public GameObject canvasCountdown;
        public GameObject canvasGameOver;
        public GameObject canvasGameOverFinish;
        public GameObject canvasGameOverPlayerScore;

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

                    if(canvasCountdown && !canvasCountdown.gameObject.activeInHierarchy) {
                        canvasCountdown.gameObject.SetActive(true);
                    }   
                }//Start

        void CheckPlayers() {

            if(PhotonNetwork.PlayerList.Length < 2) { //Se houver apenas 1 jogador na partida, o mesmo é vencedor
                //foreach (var item in PhotonNetwork.PlayerList) {
                   // GameOver();
                //}
            }
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) { //Se algum jogador sair da partida

            print(otherPlayer.NickName + " saiu da partida");
            CheckPlayers();
        }

        public void GameOver() {

            canvasGameOver.gameObject.SetActive(true);

            foreach (var item in PhotonNetwork.PlayerList) {
                
                GameObject tempPlayerScore = Instantiate(canvasGameOverPlayerScore) as GameObject;

                tempPlayerScore.transform.SetParent(canvasGameOverFinish.transform);
                tempPlayerScore.transform.position = Vector3.zero;
                tempPlayerScore.GetComponent<Nightmare.PlayerScore>().SetDados(item.NickName, item.GetScore().ToString());
            }

            foreach (var item in GameObject.FindGameObjectsWithTag("Player")) {   
                
                item.transform.Find("HUDCanvas").gameObject.SetActive(false);

                if(item.gameObject.GetComponent<PlayerMovement>()) {
                    item.gameObject.GetComponent<PlayerMovement>().enabled = false;
                }

                if(item.gameObject.GetComponent<PlayerShooting>()) {
                    item.gameObject.GetComponent<PlayerShooting>().enabled = false;
                }
            }

            canvasCountdown.gameObject.SetActive(false);
        }

    }//SCRIPTNAME
    
}//namespace


