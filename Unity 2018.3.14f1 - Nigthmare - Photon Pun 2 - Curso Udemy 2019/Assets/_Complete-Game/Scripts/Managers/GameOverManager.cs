using UnityEngine;

namespace CompleteProject
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's health.


        Animator anim;                          // Reference to the animator component.


        void Awake ()
        {
            // Set up the reference.
            anim = GetComponent <Animator> ();
        }


        void Update ()
        {
            // If the player has run out of health... //Aqui está buscando pelo player original, e faz com que o jogador Multiplayer morra assim que entra na sala, corrigir depois
           /* if(playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the game is over.
                anim.SetTrigger ("GameOver");
            } */
        }
    }
}