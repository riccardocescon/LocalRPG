using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance;




    private void Awake() {
        if(instance == null) instance = this;
    }


    void Update()
    {
        #region JoystickLeftStick
            //Jump on ColliderController script
            if(Input.GetAxis("LeftStickHorizontal2") > 0.5f){
            //PlayerAnimation.instance.SetSomething("LeftStickHorizontal2");
            //PlayerAnimation.instance.moveInput =Input.GetAxis("LeftStickHorizontal2");
            //anim.SetBool("MoveRight", true);
            } 
            else if(Input.GetAxis("LeftStickHorizontal2") < -0.5f){
                //PlayerAnimation.instance.moveInput = Input.GetAxis("LeftStickHorizontal2");
                //anim.SetBool("MoveRight", false);
            }
            else{
                //PlayerAnimation.instance.moveInput = 0f;
                //anim.SetBool("MoveRight", false);
            }
        #endregion
    
        #region JoystickButtons
            if(Input.GetButtonDown("AButton2")){
              //  Player.instance.Attack();
            }
        #endregion
    }




}
