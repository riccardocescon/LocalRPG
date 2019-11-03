using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{   
    private GameObject player;
    private Controller controller;

    public void SetPlayer(GameObject pg, Controller contr){
        player = pg;
        controller = contr;
    }


    private void OnCollisionStay2D(Collision2D other) { //player on ground
        if(player != null && controller.JoystickNum != -1){
            if(Input.GetAxis("LeftStickVertical" + controller.JoystickNum) < -0.5f && !controller.buttonAttackPressed){
                if(other.gameObject.tag == "Ground"){
                   controller.player.gameObject.transform.GetChild(1).GetComponent<AnimationHandler>().MoveVertical();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {    //Player arrived ground
        if(player != null && controller.JoystickNum != -1){
            if(other.gameObject.tag == "Ground"){
                controller.player.gameObject.transform.GetChild(1).GetComponent<AnimationHandler>().OnGround();
            }
        }
    }
    

}
