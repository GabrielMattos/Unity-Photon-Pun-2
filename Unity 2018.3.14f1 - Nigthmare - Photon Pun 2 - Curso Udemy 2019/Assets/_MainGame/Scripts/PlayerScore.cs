using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nightmare {
    
    public class PlayerScore : MonoBehaviour {

        public Text txtNickname;
        public Text txtScore;

        ///<summary>
        ///Inserir os dados para o Canvas Game Over
        ///</summary>
        public void SetDados(string nickname, string score) {

            txtNickname.text = nickname;
            txtScore.text = score;
        }

    }//SCRIPTNAME
    
}//namespace


