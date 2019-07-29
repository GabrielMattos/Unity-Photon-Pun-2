using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace ClickToMove {
    
    public class ItemController : MonoBehaviour {

        public string itemName;

        public GameObject itemList;

        private PhotonView myPhotonView;

        public RpcTarget myRpcTarget;

        void Start() {

            myPhotonView = this.GetComponent<PhotonView>();

            if(!itemList) {
                itemList = GameObject.Find("ItemList");
            }
            
        }//Start

        void Update() {

            
        }//Update

        public void HidenItem() {

            myPhotonView.RPC("HidenItemRPC", myRpcTarget);
        }

        public void ShowItem(Vector3 newPosition) {

            myPhotonView.RPC("ShowItemRPC", myRpcTarget, newPosition);
        }

        [PunRPC]
        void HidenItemRPC() {

            this.transform.parent = itemList.transform;
            this.gameObject.SetActive(false);
        }

        [PunRPC]
        void ShowItemRPC(Vector3 newPosition) {

            this.transform.parent = null;
            this.gameObject.SetActive(true);
            this.transform.position = newPosition;
        }

    }//SCRIPTNAME
    
}//namespace


