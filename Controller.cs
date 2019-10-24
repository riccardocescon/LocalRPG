using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    public int JoystickNum;



    private void Awake() {
        if(instance == null) instance = this;
    }


    void Update()
    {
        #region JoystickLeftStick
        
            //Jump on ColliderController script

            if(Input.GetAxis("LeftStickHorizontal" + JoystickNum) > 0.5f || Input.GetAxis("LeftStickHorizontal" + JoystickNum) < -0.5f){
                GetComponent<PlayerAnimation>().MoveHorizontal("LeftStickHorizontal" + JoystickNum);
            }else{
                GetComponent<PlayerAnimation>().StopMove();
            }

        #endregion
    
        #region JoystickButtons
            if(Input.GetButtonDown("AButton" + JoystickNum)){
                GetComponent<PlayerAnimation>().Attack();
            }
        #endregion
    }




}
