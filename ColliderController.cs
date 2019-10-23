using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{   
    private void OnTriggerStay2D(Collider2D other) {
        if(Input.GetAxis("LeftStickVertical2") < -0.5f){
           if(other.gameObject.tag == "Ground"){
            Player.instance.rb.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
           }
        } 
    }
}
