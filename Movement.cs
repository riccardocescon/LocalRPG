using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public int JoystickNum = -1;

    public bool buttonAttackPressed;

    public GameObject player;
    public GameObject joystickNumBuffer;




    private void Start() {
        joystickNumBuffer = GameObject.Find("/JoystickBuffer");
        JoystickNum = joystickNumBuffer.GetComponent<JoystickBuffer>().GetJoystickNumber();

        player = joystickNumBuffer.GetComponent<JoystickBuffer>().GetPlayer(JoystickNum);
        player.transform.GetChild(0).GetChild(2).GetComponent<ColliderController>().SetPlayer(player, JoystickNum);
        GetComponent<Player>().SetData();
    }


    void Update()
    {

        if(player != null && JoystickNum != -1){
            #region JoystickLeftStick
            
                //Jump on ColliderController script

                if(!buttonAttackPressed){
                if(Input.GetAxis("LeftStickHorizontal" + JoystickNum) > 0.5f || Input.GetAxis("LeftStickHorizontal" + JoystickNum) < -0.5f){
                        player.transform.GetChild(1).GetComponent<AnimationHandler>().MoveHorizontal("LeftStickHorizontal" + JoystickNum);
                    }else{
                        player.transform.GetChild(1).GetComponent<AnimationHandler>().StopMove();
                    } 
                }
                
                

            #endregion
        
            #region JoystickButtons
                if(Input.GetButtonDown("AButton" + JoystickNum) && !buttonAttackPressed){

                    player.transform.GetChild(1).GetComponent<AnimationHandler>().Attack(1);

                }else if(Input.GetButtonDown("BButton" + JoystickNum))
                {
                    player.transform.GetChild(1).GetComponent<AnimationHandler>().Attack(2);
                    

                    buttonAttackPressed = true;
                }
                if(Input.GetButtonUp("BButton" + JoystickNum)){
                    
                    player.transform.GetChild(1).GetComponent<AnimationHandler>().ReleaseAttack();

                    buttonAttackPressed = false;
                }

                
            #endregion
        }
    }
}
