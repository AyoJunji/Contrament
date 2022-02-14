using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    Game, Pause, Lose, Win
}
public class LevelManager : MonoBehaviour
{
    public static GameState gamestate;
    public static Animator tutorialAnim;
    public static LevelManager instance;
    [SerializeField] private Animator exitButtonPauseAnim;
    [SerializeField] private Animator exitButton2Anim;
    [SerializeField] private Animator restartGameButtonAnim;
    public Animator messageAnim;


    private void Start() {
        instance = this;
        tutorialAnim = GameObject.Find("Tutorial").GetComponent<Animator>();
        gamestate = GameState.Pause;    

    }

    private void Update() {
        
        if(Input.GetKey(KeyCode.Return) && gamestate == GameState.Pause) {
            tutorialAnim.SetTrigger("fadeOut");
            LevelManager.gamestate = GameState.Game;
        }
        if(Input.GetKey(KeyCode.Escape) && gamestate == GameState.Pause) {
            Invoke("GoToTitle", 1);
        }
        if(Input.anyKeyDown && gamestate == GameState.Lose) {
            Invoke("RestartGame", 2);
        }
        if(Input.anyKeyDown && gamestate == GameState.Win) {
            GoToTitle();
        }
    }
    public void GoToTitle() {
        SceneManager.LoadScene(0);
    }
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void InvokeNextLevel(float s) {
        Invoke("LoadNextLevel", s);
    }
    public void LoadNextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void exitGamePauseMouseOver() {
        exitButtonPauseAnim.SetBool("mouseOver", true);
    }
    public void exitGamePauseMouseGone() {
        exitButtonPauseAnim.SetBool("mouseOver", false);
    }
    public void restartGameMouseOver() {
        restartGameButtonAnim.SetBool("mouseOver", true);
    }
    public void restartGameMouseGone() {
        restartGameButtonAnim.SetBool("mouseOver", false);
    }
    public void exitGame2MouseOver() {
        exitButton2Anim.SetBool("mouseOver", true);
    }
    public void exitGame2MouseGone() {
        exitButton2Anim.SetBool("mouseOver", false);
    }
    
}
