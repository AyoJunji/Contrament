using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
