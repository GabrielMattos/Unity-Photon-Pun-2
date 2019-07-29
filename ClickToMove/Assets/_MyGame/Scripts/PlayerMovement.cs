using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ClickToMove {
    
    public class PlayerMovement : MonoBehaviour {

        private NavMeshAgent myNavMeshAgent;

        void Awake() {

            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Start() {

            
        }//Start

        void Update() {

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


