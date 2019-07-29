using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace ClickToMove {
    
    public class ItemSwitch : MonoBehaviour {

        public Transform weapon;

        //Tempo para pegar o item
        float timeForTake = 0.5f;
        public float timeForTakeCurrent = 0f;

        private PhotonView myPhotonView;

        public RpcTarget myRpcTarget;

        void Start() {

            myPhotonView = GetComponent<PhotonView>();
            
        }//Start

        void Update() {

            timeForTakeCurrent += Time.deltaTime;
        }//Update

        private void OnTriggerStay(Collider target) {
            
            if(!myPhotonView.IsMine) {
                return;
            }

            if(target.CompareTag("Item") && Input.GetKey(KeyCode.F) && timeForTakeCurrent >= timeForTake) {
                timeForTakeCurrent = 0f;
                myPhotonView.RPC("TakeItemRPC", myRpcTarget, target.GetComponent<ItemController>().itemName);
            }
        }

        [PunRPC]
        void TakeItemRPC(string nameItem) {

            foreach (Transform itemWeapon in weapon)
            {
                if(itemWeapon.name == nameItem)
                {
                    if(!itemWeapon.gameObject.activeInHierarchy)
                    {
                        itemWeapon.gameObject.SetActive(true);
                    }
                } 
                else 
                {
                    itemWeapon.gameObject.SetActive(false);
                } 
            }
        }

    }//SCRIPTNAME
    
}//namespace


