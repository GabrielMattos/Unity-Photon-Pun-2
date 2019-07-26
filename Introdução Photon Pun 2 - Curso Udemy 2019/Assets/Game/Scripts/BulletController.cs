using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace IntroducaoPhotonUdemy  {

        public class BulletController : MonoBehaviour {

            public float bulletSpeed;
            public Rigidbody2D myrb;

            public float bulletLifeTime;
            public float bulletTimeCount;

            public float bulletDamage = 10f;

            // Start is called before the first frame update
            void Start()
            {
                myrb = GetComponent<Rigidbody2D>();

                myrb.AddForce(transform.up * bulletSpeed, ForceMode2D.Force);
            }

            // Update is called once per frame
            void Update() {

                if(bulletTimeCount >= bulletLifeTime) {
                    Destroy(this.gameObject);
                }

                bulletTimeCount += Time.deltaTime;
            }

            [PunRPC]
            void BulletDestroy() {

                Destroy(this.gameObject);
            }

            private void OnTriggerEnter2D(Collider2D target) {

                if(target.CompareTag("Player") && target.GetComponent<PlayerController>() && target.GetComponent<PhotonView>().IsMine) {
                    print("PlayerID: " + target.GetComponent<PhotonView>().Owner.ActorNumber + " PlayerName: " + target.GetComponent<PhotonView>().Owner.NickName);
                    target.GetComponent<PlayerController>().TakeDamage(-bulletDamage, GetComponent<PhotonView>().Owner);
                    this.GetComponent<PhotonView>().RPC("BulletDestroy", RpcTarget.AllViaServer); //Aqui não está destruindo para novos jogadores que entram na sala - corrigir depois
                }

                Destroy(this.gameObject);
            }
        }   
}



