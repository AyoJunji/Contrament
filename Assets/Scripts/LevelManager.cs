using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    Game, Pause
}
public class LevelManager : MonoBehaviour
{
    public static GameState gamestate;
    public static Animator tutorialAnim;
    [SerializeField] private Animator exitButtonPauseAnim;


    private void Start() {
        tutorialAnim = GameObject.Find("Tutorial").GetComponent<Animator>();
        gamestate = GameState.Pause;       
    }

    private void Update() {
        
        if(Input.GetKey(KeyCode.Return) && gamestate == GameState.Pause) {
            tutorialAnim.SetTrigger("fadeOut");
            LevelManager.gamestate = GameState.Game;
        }
    }
    public void GoToTitle() {
        SceneManager.LoadScene(0);
    }
    public void exitGamePauseMouseOver() {
        exitButtonPauseAnim.SetBool("mouseOver", true);
    }
    public void exitGamePauseMouseGone() {
        exitButtonPauseAnim.SetBool("mouseOver", false);
    }
}
