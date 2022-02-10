using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Buttons_Controller : MonoBehaviour
{
    [SerializeField] private Animator playButtonAnim;
    [SerializeField] private Animator exitButtonAnim;

    public void playMouseOver() {
        playButtonAnim.SetBool("mouseOver", true);
    }
    public void playMouseGone() {
        playButtonAnim.SetBool("mouseOver", false);
    }
    public void exitMouseOver() {
        exitButtonAnim.SetBool("mouseOver", true);
    }
    public void exitMouseGone() {
        exitButtonAnim.SetBool("mouseOver", false);
    }

    public void StartGame(float s) {
        Invoke("LoadScene", s);
    }
    void LoadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LeaveGame(float s) {
        Invoke("QuitApp", s);
    }
    void QuitApp() {
        Application.Quit();
    }
}
