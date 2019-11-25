using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public int JoystickNum = -1;

    public bool buttonAttackPressed;

    public GameObject player;
    public GameObject joystickNumBuffer;
    public bool pause = false;



    private void Start() {
        joystickNumBuffer = GameObject.Find("/JoystickBuffer");
        JoystickNum = joystickNumBuffer.GetComponent<JoystickBuffer>().GetJoystickNumber();

        player = joystickNumBuffer.GetComponent<JoystickBuffer>().GetPlayer(JoystickNum);
        player.transform.GetChild(0).GetChild(0).GetComponent<ColliderController>().SetPlayer(player, JoystickNum);
        GetComponent<Player>().SetData();
    }

    public void MoveManager(string move){
        switch(move){
            case "Move":
                switch(this.gameObject.GetComponent<Player>().lastClassUsed){
                    case "Warrior":
                        player.transform.GetChild(1).GetComponent<AnimationHandler>().MoveHorizontal("LeftStickHorizontal" + JoystickNum);
                    break;

                    case "Ranger":
                        player.transform.GetChild(1).GetComponent<RangerAnimation>().MoveHorizontal("LeftStickHorizontal" + JoystickNum);
                    break;
                }

            break;

            case "Attack1":
                switch(this.gameObject.GetComponent<Player>().lastClassUsed){
                    case "Warrior":
                        player.transform.GetChild(1).GetComponent<AnimationHandler>().Attack(1);
                    break;

                    case "Ranger":
                        player.transform.GetChild(1).GetComponent<RangerAnimation>().Attack(1);
                    break;
                }
            break;

            case "Attack2":
                switch(this.gameObject.GetComponent<Player>().lastClassUsed){
                    case "Warrior":
                        player.transform.GetChild(1).GetComponent<AnimationHandler>().Attack(2);
                    break;

                    case "Ranger":
                        player.transform.GetChild(1).GetComponent<RangerAnimation>().Attack(2);
                    break;
                }
            break;

            case "StopMove":
                switch(this.gameObject.GetComponent<Player>().lastClassUsed){
                    case "Warrior":
                        player.transform.GetChild(1).GetComponent<AnimationHandler>().StopMove();
                    break;

                    case "Ranger":
                        player.transform.GetChild(1).GetComponent<RangerAnimation>().StopMove();
                    break;
                }
            break;

            case "ReleaseAttack":
                switch(this.gameObject.GetComponent<Player>().lastClassUsed){
                    case "Warrior":
                        player.transform.GetChild(1).GetComponent<AnimationHandler>().ReleaseAttack();
                    break;

                    case "Ranger":
                        //Non ha attacchi da rilasciare
                    break;
                }
            break;

            case "Jump":
                switch(this.gameObject.GetComponent<Player>().lastClassUsed){
                    case "Warrior":
                        player.gameObject.transform.GetChild(1).GetComponent<AnimationHandler>().MoveVertical();
                    break;

                    case "Ranger":
                        player.gameObject.transform.GetChild(1).GetComponent<RangerAnimation>().MoveVertical();
                    break;
                }
            break;

            case "OnGround":
                switch(this.gameObject.GetComponent<Player>().lastClassUsed){
                    case "Warrior":
                        player.gameObject.transform.GetChild(1).GetComponent<AnimationHandler>().OnGround();
                    break;

                    case "Ranger":
                        player.gameObject.transform.GetChild(1).GetComponent<RangerAnimation>().OnGround();
                    break;
                }
            break;

        }
    }


    void Update()
    {
        if(!pause){
            if(player != null && JoystickNum != -1){
            #region JoystickLeftStick
            
                //Jump on ColliderController script

                if(!buttonAttackPressed){
                    if(Input.GetAxis("LeftStickHorizontal" + JoystickNum) > 0.5f || Input.GetAxis("LeftStickHorizontal" + JoystickNum) < -0.5f){
                        MoveManager("Move");
                    }else{
                        MoveManager("StopMove");
                    } 
                }   
            } 

            #endregion
        
            #region JoystickButtons
                if(Input.GetButtonDown("AButton" + JoystickNum) && !buttonAttackPressed){

                    MoveManager("Attack1");

                }else if(Input.GetButtonDown("BButton" + JoystickNum))
                {
                    MoveManager("Attack2");
                    

                    buttonAttackPressed = true;
                }
                if(Input.GetButtonUp("BButton" + JoystickNum)){
                    
                    MoveManager("ReleaseAttack");

                    buttonAttackPressed = false;
                }

                
            #endregion
        }
    }
       
}
