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
    public bool attacking = false;

    private float attackDelay;
    private float currentTime = 1;

    private float secondAttackDelay;
    private float secondTime = 1;
    private bool canMove = true;

    public bool inflict = false;    //Viene usato per evitare che togla vita ad ogni collider dei  nemici, usato su SwordScript





    private void Awake() {
        if(instance == null)instance = this;
    }

    private void Start() {
        classParent = this.transform.parent.gameObject;
        anim = classParent.gameObject.GetComponent<Animator>();
        secondAttackDelay = 1;
    }

    private void Update() {
        if(moveInput > 0.5f || moveInput < -0.5f){
            //rightmove animaiton
            anim.SetBool("Move", true);
        }else{
            anim.SetBool("Move", false);
        } 
        currentTime -= Time.deltaTime;
        secondTime -= Time.deltaTime;
        if(currentTime <= 0f && secondTime <= secondAttackDelay - 1){
           canMove = true;
           attacking = false;
           inflict = false;
        } 
        if(classParent.gameObject.GetComponent<Player>().currentAction == "Defending" && classParent.gameObject.GetComponent<Player>().mana > 0){
            classParent.GetComponent<Player>().ManaAttack(8f/60f);//8 al secondo
        }else if(classParent.GetComponent<Player>().mana < classParent.GetComponent<Player>().startMana){
            classParent.GetComponent<Player>().ManaAttack(-2f/60f);//Recupera 2 al secondo
        }
        if(classParent.gameObject.GetComponent<Player>().currentAction == "Defending" && classParent.gameObject.GetComponent<Player>().mana < 0){
            ReleaseAttack();
        }
        if(secondTime <= 0){
            classParent.GetComponent<Player>().SetSecondAttackState(true);
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
        if(!canMove) return;
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
        canMove = false;
        if(num == 1 && currentTime <= 0){
            attacking = true;
            anim.Play(classParent.gameObject.GetComponent<Player>().lastClassUsed + "Attack1");
            //currentTime = attackDelay;
            currentTime = classParent.GetComponent<Player>().speedAttack;
        } 
        else if(num == 2 && secondTime <= 0 && classParent.gameObject.GetComponent<Player>().mana > 0){
           anim.Play(classParent.gameObject.GetComponent<Player>().lastClassUsed + "Attack2");
           if(classParent.gameObject.GetComponent<Player>().lastClassUsed == "Warrior"){      //Aggiungi con un or tutte le classi che possono rilasciare un attacco qui e su ReleaseAttack
                anim.SetBool("Released", false);
                classParent.gameObject.GetComponent<Player>().currentAction = "Defending";
                secondTime = secondAttackDelay;
                classParent.GetComponent<Player>().SetSecondAttackState(false);
           } 

        }
    }

    public void ReleaseAttack(){
        if(classParent.gameObject.GetComponent<Player>().lastClassUsed == "Warrior" ) anim.SetBool("Released", true);
         classParent.gameObject.GetComponent<Player>().currentAction = "None";
    }

}
