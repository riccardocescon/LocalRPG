using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimation : MonoBehaviour
{
    public static EndAnimation instance;
    private Animator animator;
    public int JoystickNum;
    private bool catchButton = false;


    private void Awake() {
        if(instance == null) instance = this;
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(catchButton){
            if(Input.GetButtonDown("AButton" + JoystickNum)){
                GameObject.Find("/MatchManager").GetComponent<MatchManager>().Ready(JoystickNum - 1);
            }
        }
    }


    public void Animate(string playerClass, int num, bool win){
        Debug.Log(playerClass + num);
        if(win){
            animator.Play(playerClass + "Win" + num);
        }else{
            animator.Play(playerClass + "Lose" + num);
        }
        catchButton = true;
    }

}
