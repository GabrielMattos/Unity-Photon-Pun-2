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

        private bool hidenItem = false;

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
                if(hidenItem) {
                    target.GetComponent<ClickToMove.ItemController>().HidenItem();
                    hidenItem = false;
                }
                
            }
        }

        [PunRPC]
        void TakeItemRPC(string nameItem) {

            foreach (Transform itemWeapon in weapon) {
                if(itemWeapon.name == nameItem) {
                    if(!itemWeapon.gameObject.activeInHierarchy) {
                        itemWeapon.gameObject.SetActive(true);
                        hidenItem = true;
                    }

                } else if(itemWeapon.gameObject.activeInHierarchy) {
                    itemWeapon.gameObject.SetActive(false);
                    foreach (Transform itemList in GameObject.Find("ItemList").transform) {
                        if(itemList.GetComponent<ItemController>().itemName == itemWeapon.name && !itemList.gameObject.activeInHierarchy) {
                            itemList.GetComponent<ItemController>().ShowItem(this.transform.position);
                        }
                    }
                } else {
                    itemWeapon.gameObject.SetActive(false);
                } 
            }
        }

    }//SCRIPTNAME
    
}//namespace


