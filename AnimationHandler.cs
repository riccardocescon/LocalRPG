using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public static AnimationHandler instance;

    public float moveInput = 0;
    private float horizontalForce = 0;
    private bool facingRight = true;
    private GameObject classParent;
    public Animator anim;




    private void Awake() {
        if(instance == null)instance = this;
    }

    private void Start() {
        classParent = this.transform.parent.gameObject;
        anim = classParent.gameObject.GetComponent<Animator>();
    }

    private void Update() {
        if(moveInput > 0.5f || moveInput < -0.5f){
            //rightmove animaiton
            anim.SetBool("Move", true);
        }else{
            anim.SetBool("Move", false);
        } 
    }

    private void FixedUpdate() {
        for(int i = 0; i < 4; i++){
            if(!classParent.gameObject.activeSelf) break;
            classParent.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalForce * classParent.gameObject.GetComponent<Player>().speed, classParent.gameObject.GetComponent<Rigidbody2D>().velocity.y);                


            if(horizontalForce > 0){
                horizontalForce -=  2 * Time.deltaTime;
            }else if(horizontalForce < 0){
                horizontalForce += 2 * Time.deltaTime;
            }


        }
    }


    public void MoveHorizontal(string button){
        moveInput = Input.GetAxis(button);
        if(moveInput < -0.5f && facingRight) Flip();
        if(moveInput > 0.5f && !facingRight) Flip();
        horizontalForce = moveInput;
    }

     private void Flip(){
        facingRight = !facingRight;
        classParent.gameObject.transform.localScale = new Vector3(-1 * classParent.gameObject.transform.localScale.x, classParent.gameObject.transform.localScale.y, classParent.gameObject.transform.localScale.z);
    }

        public void StopMove(){
        moveInput = 0;
    }

    public void MoveVertical(){
        classParent.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        anim.SetBool("OnGround", false);
    }

    public void OnGround(){
        anim.SetBool("OnGround", true);
    }

    public void Attack(int num){
        
        //Attack Animation
        if(num == 1){
            anim.Play(classParent.gameObject.GetComponent<Player>().lastClassUsed + "Attack1");
        } 
        else{
           anim.Play(classParent.gameObject.GetComponent<Player>().lastClassUsed + "Attack2");
           if(classParent.gameObject.GetComponent<Player>().lastClassUsed == "Warrior"){      //Aggiungi con un or tutte le classi che possono rilasciare un attacco qui e su ReleaseAttack
                anim.SetBool("Released", false);
                classParent.gameObject.GetComponent<Player>().currentAction = "Defending";
           } 

        } 
    }

    public void ReleaseAttack(){
        if(classParent.gameObject.GetComponent<Player>().lastClassUsed == "Warrior" ) anim.SetBool("Released", true);
    }

}
