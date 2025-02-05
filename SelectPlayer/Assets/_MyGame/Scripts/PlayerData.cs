﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SelectPlayerPhoton
{
    
    [CreateAssetMenu(fileName = "new PlayerConfig", menuName = "PlayerData")]    
    public class PlayerData : ScriptableObject {

        public Sprite playerIcon;
        public string playerName;
        public string playerStatus;
        public string playerType;
        public string playerSpeed;

    }//SCRIPTNAME
}



