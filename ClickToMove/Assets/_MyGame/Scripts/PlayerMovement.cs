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

    }//SCRIPTNAME
    
}//namespace


