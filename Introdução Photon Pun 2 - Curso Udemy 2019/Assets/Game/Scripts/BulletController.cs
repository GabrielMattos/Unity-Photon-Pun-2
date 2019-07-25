using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IntroducaoPhotonUdemy  {

        public class BulletController : MonoBehaviour {

            public float bulletSpeed;
            public Rigidbody2D myrb;

            public float bulletLifeTime;
            public float bulletTimeCount;

            // Start is called before the first frame update
            void Start()
            {
                myrb = GetComponent<Rigidbody2D>();

                myrb.AddForce(transform.up * bulletSpeed, ForceMode2D.Force);
            }

            // Update is called once per frame
            void Update()
            {
                if(bulletTimeCount >= bulletLifeTime) {
                    Destroy(this.gameObject);
                }

                bulletTimeCount += Time.deltaTime;
            }
        }   
}



