using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClickToMove {
    
    public class PlayerSwitch : MonoBehaviour {

        void Start() {


        }//Start

        public void BtnV1() {

            Character(0);
        }

        public void BtnV2() {

            Character(1);
        }

        public void BtnV3() {

            Character(2);
        }

        void Character(int value) {

            PlayerPrefs.SetInt("CHARACTER", value);
            SceneManager.LoadScene("SampleScene");
        }

    }//SCRIPTNAME
    
}//namespace


