using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SelectPlayerPhoton {
    
    public class PlayerConfig : MonoBehaviour {

        public PlayerData playerData;

        public Sprite playerIcon;

        void Start() {

            playerIcon = playerData.playerIcon;
        }

    }//SCRIPTNAME
    
}//namespace


