using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

namespace ClickToMove {
    
    public class PlayerMovement : MonoBehaviour {

        private NavMeshAgent myNavMeshAgent;

        PhotonView myPhotonView;

        int characterCurrent;

        public GameObject body;

        public RpcTarget myRpcTarget;

        void Awake() {

            myNavMeshAgent = GetComponent<NavMeshAgent>();
            myPhotonView = GetComponent<PhotonView>();
        }

        void Start() {

            foreach (Transform item in this.transform) {
                if(item.name == "Camera") {
                    item.parent = null;
                    if(myPhotonView.IsMine) {
                        item.gameObject.SetActive(true);
                    }
                }
            }

             characterCurrent = PlayerPrefs.GetInt("CHARACTER", 0);

            if(myPhotonView.IsMine) {
                myPhotonView.RPC("SwitchCaracterRPC", myRpcTarget, characterCurrent);
            }

           

        }//Start

        void Update() {

            if(!myPhotonView.IsMine) {
                return;
            }

            ClickToMove();
        }//Update

        void ClickToMove() {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Input.GetMouseButtonDown(0)) {
                if(Physics.Raycast(ray, out hit, 100)) {
                    myNavMeshAgent.destination = hit.point;
                }
            }
        }

        [PunRPC]
        void SwitchCaracterRPC(int value) {

            int i = 0;

            foreach (Transform characters in body.transform)
            {
                if(i == value) {
                    characters.gameObject.SetActive(true);
                } else {
                    characters.gameObject.SetActive(false);
                }

                i++;
            }
        }

    }//SCRIPTNAME
    
}//namespace


