using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone_Controller : MonoBehaviour
{
    [SerializeField] private bool endLevel;
    [SerializeField] private bool endGame; 
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Player"))
            {
            if(endLevel) {
                PlayerController.playerControllerCS.WinLevel();
            }
            if(endGame) {
                PlayerController.playerControllerCS.WinGame();
            }
        }
   }
}
