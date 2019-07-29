using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts; //Para pegar score
using System.Linq; //organização dos valores da pontuação
using UnityEngine.SceneManagement;

namespace Nigthmare {
    
    public class GameControllerGameplay : MonoBehaviourPunCallbacks {

        public GameObject myPlayer;
        public Transform[] spawnPlayer;

        public GameObject canvasCountdown;
        public GameObject canvasGameOver;
        public GameObject canvasGameOverFinish;
        public GameObject canvasGameOverPlayerScore;

        bool isGameOver = false;

        void Start() {

            isGameOver = false;

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
                   
                   
                   
                   GameOver();
            }
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) { //Se algum jogador sair da partida

            print(otherPlayer.NickName + " saiu da partida");

            if(isGameOver) {
                return;     
            }
            CheckPlayers();
        }

        public void GameOver() {

            canvasGameOver.gameObject.SetActive(true);

            var dictionary = new Dictionary<string, int>();

            foreach (var item in PhotonNetwork.PlayerList) {
                
                /*GameObject tempPlayerScore = Instantiate(canvasGameOverPlayerScore) as GameObject;

                tempPlayerScore.transform.SetParent(canvasGameOverFinish.transform);
                tempPlayerScore.transform.position = Vector3.zero;
                tempPlayerScore.GetComponent<Nightmare.PlayerScore>().SetDados(item.NickName, item.GetScore().ToString()); */

                dictionary.Add(item.NickName, item.GetScore()); //organização dos valores da pontuação
            }

            var items = from pair in dictionary orderby pair.Value descending select pair; //organização dos valores da pontuação

            foreach (var item in items)
            {
                GameObject tempPlayerScore = Instantiate(canvasGameOverPlayerScore) as GameObject;

                tempPlayerScore.transform.SetParent(canvasGameOverFinish.transform);
                tempPlayerScore.transform.position = Vector3.zero;
                tempPlayerScore.GetComponent<Nightmare.PlayerScore>().SetDados(item.Key, item.Value.ToString());
            }

            canvasCountdown.gameObject.SetActive(false);

            //Propriedades da sala
            ExitGames.Client.Photon.Hashtable myProps = new ExitGames.Client.Photon.Hashtable {
                {"isGameOver", true}
                }; 
            PhotonNetwork.CurrentRoom.SetCustomProperties(myProps);

            isGameOver = true;

            //Debug.Log("Passei por aqui!");
        }

        //Botão sair do jogo
        public void BtnDesconnectar() {

            PhotonNetwork.LeaveRoom();
            
        }

        public override void OnDisconnected(DisconnectCause cause) {
            SceneManager.LoadScene("Lobby");
        }

        public override void OnLeftRoom() {
            PhotonNetwork.Disconnect();
        }

    }//SCRIPTNAME
    
}//namespace


