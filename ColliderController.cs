using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{   
    private GameObject player;
    private int JoystickNum;


    private void Start() {
        
    }

    public void SetPlayer(GameObject pg, int _JoystickNum){
        player = pg;
        JoystickNum = _JoystickNum;
    }


    private void OnCollisionStay2D(Collision2D other) { //player on ground
        if(player != null && JoystickNum != -1){
            if(Input.GetAxis("LeftStickVertical" + JoystickNum) < -0.5f){
                if(other.gameObject.tag == "Ground"){
                   player.gameObject.transform.GetChild(1).GetComponent<AnimationHandler>().MoveVertical();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {    //Player arrived ground
        if(player != null && JoystickNum != -1){
            if(other.gameObject.tag == "Ground"){
                player.gameObject.transform.GetChild(1).GetComponent<AnimationHandler>().OnGround();
            }
        }
    }
    

}
