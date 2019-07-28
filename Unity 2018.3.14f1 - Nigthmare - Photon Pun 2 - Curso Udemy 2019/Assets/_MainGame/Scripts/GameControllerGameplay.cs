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
            PhotonNetwork.Instantiate(myPlayer.name, spawnPlayer[tempPosition].position, spawnPlayer[tempPosition].rotation);
        }//Start

    }//SCRIPTNAME
    
}//namespace


