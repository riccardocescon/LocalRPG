using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderController : MonoBehaviour
{   
    private GameObject player;
    private int JoystickNum;
    [Header("Unity Stuff")]
    public Image healthBar;
    public Image manaBar;
    public bool alive = true;


    private void Start() {
        
    }

    public void TakeDamage(float amount){
        if(!alive) return;
        float health = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Player>().health;
        float startHealth = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Player>().startHealth;
        if(healthBar != null){healthBar.fillAmount = health / startHealth;}
    }

    public void UseMana(float amount){
        float mana = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Player>().mana;
        float startMana = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Player>().startMana;
        if(manaBar != null)manaBar.fillAmount = mana / startMana;
    }

    public void SetPlayer(GameObject pg, int _JoystickNum){
        player = pg;
        JoystickNum = _JoystickNum;
    }


    private void OnCollisionStay2D(Collision2D other) { //player on ground
        if(player != null && JoystickNum != -1 && !this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Movement>().pause){
            if(Input.GetAxis("LeftStickVertical" + JoystickNum) < -0.5f){
                if(other.gameObject.tag == "Ground"){
                   this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Movement>().MoveManager("Jump");
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {    //Player arrived ground
        if(player != null && JoystickNum != -1){
            if(other.gameObject.tag == "Ground"){
                 this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Movement>().MoveManager("OnGround");
            }
        }
    }

    public void Die(){
        alive = false;
    }

    
    

}
