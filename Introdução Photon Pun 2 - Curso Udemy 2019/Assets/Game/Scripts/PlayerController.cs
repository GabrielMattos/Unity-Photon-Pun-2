using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

namespace IntroducaoPhotonUdemy  {

    public class PlayerController : MonoBehaviourPun /*IPunObservable */ {

        public float playerSpeed;

        public Rigidbody2D myrb;

        private PhotonView myPhotonView;

        public float playerHealthMax = 100f, playerCurrentHealth;
        public Image playerHealthFill;

        public GameObject spawnBullet;
        public GameObject bulletPrefab;
        public GameObject bulletPrefabPhotonView;

        private bool gameIsOver;

        // Start is called before the first frame update
        void Start() {

            myrb = GetComponent<Rigidbody2D>();
            myPhotonView = GetComponent<PhotonView>();
            HealthManager(playerHealthMax);
            gameIsOver = false;
        }

        // Update is called once per frame
        void Update() {

            if(myPhotonView.IsMine) { //Para controlar apenas o seu personagem e não dos outros jogadores
                PlayerMove();
                PlayerTurn();
                Shooting();
            }

            //if(Input.GetMouseButtonDown(0)) {
              //  HealthManager(-10f);
            //}
        }

        void Shooting() {

            if(Input.GetMouseButtonDown(0)) {
                
                //Chama a função para ser vista na rede
                //photonView.RPC("Shoot", RpcTarget.All);
                PhotonNetwork.Instantiate(bulletPrefab.name, spawnBullet.transform.position, spawnBullet.transform.rotation, 0);
            }

            /*if(Input.GetMouseButtonDown(1)) {
                
                PhotonNetwork.Instantiate(bulletPrefabPhotonView.name, spawnBullet.transform.position, spawnBullet.transform.rotation, 0); //Instanciando objetos na rede, podendo ser visto por outros jogadores
            } */
        }

        [PunRPC] //Isso quer dizer que a função é pra ser vista na rede
        void Shoot() {

            Instantiate(bulletPrefab, spawnBullet.transform.position, spawnBullet.transform.rotation);
        }

        public void TakeDamage(float value, Player playerTemp) {

            photonView.RPC("TakeDamageNetwork", RpcTarget.AllBuffered, value, playerTemp);
        }

        [PunRPC]
        void TakeDamageNetwork(float value, Player playerTemp) {

            HealthManager(value);
            
            object playerScoreTempGet;
            playerTemp.CustomProperties.TryGetValue("Score", out playerScoreTempGet);

            int soma = (int) playerScoreTempGet;
            soma += 10;

            ExitGames.Client.Photon.Hashtable playerHashtableTemp = new ExitGames.Client.Photon.Hashtable();
            playerHashtableTemp.Add("Score", soma);

            playerTemp.SetCustomProperties(playerHashtableTemp, null, null);

            playerTemp.AddScore(10);

            if(playerCurrentHealth <= 0f && myPhotonView.IsMine) {
                //Aqui deveria estar enviando só pro Master, porém quando o master vence nao funciona
                photonView.RPC("IsGameOver", RpcTarget.All); //Avisa ao master que o jogo acabou, pois o jogador tem o HP menor ou igual a zero
            }

            //if(gameIsOver) {
              //  playerCurrentHealth = 0f;
                //print("ACABOU! JÁ ERA!");
            //}
        }

        [PunRPC] 
        void IsGameOver() { //No momento aqui encontra-ser um bug - o jogador Master o qual cria a sala, não consegue ser o vencedor da partida.
            
                foreach (var item in PhotonNetwork.PlayerList) {

                    object playerScoreTempGet;
                    item.CustomProperties.TryGetValue("Score", out playerScoreTempGet);
                    print("Player Name: " + item.NickName + " | Score: " + playerScoreTempGet.ToString() + " | Score via Photon: " + item.GetScore());
                }
            
            
            
            
            /*print("PASSEI POR AQUI!");

            if(!myPhotonView.Owner.IsMasterClient) {
                print("Meu nome: " + PhotonNetwork.NickName + " | Não sou o master!");
                print(PhotonNetwork.MasterClient.NickName + " é o MASTER");
            }

            if(myPhotonView.Owner.IsMasterClient) { //Apenas o Master executa
                print("NOME: " + PhotonNetwork.NickName + " Sou Master! GameOver");

                foreach (var item in PhotonNetwork.PlayerList) {

                    object playerScoreTempGet;
                    item.CustomProperties.TryGetValue("Score", out playerScoreTempGet);
                    print("Player Name: " + item.NickName + " | Score: " + playerScoreTempGet.ToString() + " | Score via Photon: " + item.GetScore());
                }
            } */
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

/*         //Envio e recebimento de informações
        // Necessário MonoBehaviourPun, IPunObservable e script onde isso ocorre no componente PhotonView
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

            if(stream.IsWriting) {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
                stream.SendNext(myrb.velocity);
            } else {
                Vector3 temp = (Vector3)stream.ReceiveNext();
                transform.position = Vector3.Lerp(transform.position, temp, Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime)));

                transform.rotation = (Quaternion)stream.ReceiveNext();

                myrb.velocity = (Vector2)stream.ReceiveNext();

                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                myrb.position += myrb.velocity * lag;
            }
        }*/

    }


}



