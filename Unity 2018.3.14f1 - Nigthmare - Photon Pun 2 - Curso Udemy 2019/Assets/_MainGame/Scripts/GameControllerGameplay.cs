using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Nigthmare {
    
    public class GameControllerGameplay : MonoBehaviour {

        public GameObject myPlayer;
        public Transform[] spawnPlayer;

        void Start() {

            int tempPosition = Random.Range(0, spawnPlayer.Length);
            GameObject playerTemp = PhotonNetwork.Instantiate(myPlayer.name, spawnPlayer[tempPosition].position, spawnPlayer[tempPosition].rotation, 0) as GameObject;

                //Iniciando Countdownendgame
                //foreach (var item in PhotonNetwork.PlayerList) {
                    //if(item.IsMasterClient) {
                    if(playerTemp.GetComponent<PhotonView>().Owner.IsMasterClient) {
                        ExitGames.Client.Photon.Hashtable myProps = new ExitGames.Client.Photon.Hashtable {
                        {CountdownEndGame.CountdownStartTime, (float)PhotonNetwork.Time}
                        }; 
                        PhotonNetwork.CurrentRoom.SetCustomProperties(myProps);
                    }   
                }//Start

    }//SCRIPTNAME
    
}//namespace


