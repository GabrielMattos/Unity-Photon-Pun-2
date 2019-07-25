using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace IntroducaoPhotonUdemy  {

    public class PlayerController : MonoBehaviour {

        public float playerSpeed;

        private Rigidbody2D myrb;

        private PhotonView myPhotonView;

        public float playerHealthMax = 100f, playerCurrentHealth;
        public Image playerHealthFill;

        // Start is called before the first frame update
        void Start() {

            myrb = GetComponent<Rigidbody2D>();
            myPhotonView = GetComponent<PhotonView>();
            HealthManager(playerHealthMax);
        }

        // Update is called once per frame
        void Update() {

            if(myPhotonView.IsMine) { //Para controlar apenas o seu personagem e não dos outros jogadores
                PlayerMove();
                PlayerTurn();
            }

            if(Input.GetMouseButtonDown(0)) {
                HealthManager(-10f);
            }
        }

        
        void HealthManager(float value) {
            playerCurrentHealth += value;
            playerHealthFill.fillAmount = playerCurrentHealth / 100;
        } 

        void PlayerMove() {

            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            myrb.velocity = new Vector2(x, y);
        }

        
        void PlayerTurn() {

            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 diretion = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            transform.up = diretion;
        }
    }


}



