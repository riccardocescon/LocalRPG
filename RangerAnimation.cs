﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAnimation : MonoBehaviour
{
    public static RangerAnimation instance;

    public float moveInput = 0;
    private float horizontalForce = 0;
    private bool facingRight = true;
    private GameObject classParent;
    public Animator anim;

    private float attackDelay;
    private float currentTime = 1;

    private float secondAttackDelay;
    private float secondTime = 5;
    private bool canMove = true;
    private bool powerCheck = false;





    private void Awake() {
        if(instance == null)instance = this;
    }

    private void Start() {
        classParent = this.transform.parent.gameObject;
        attackDelay = classParent.GetComponent<Player>().speedAttack;   //Non assegna il valore alla variabile, quindi uso direttamente classParent.GetComponent<Player>().speedAttack
        secondAttackDelay = 10;
        anim = classParent.gameObject.GetComponent<Animator>();
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
        if(currentTime <= -0.5f && secondTime <= secondAttackDelay - 1) canMove = true;                     //SOSTITUISCI IL COMMENTO CON .....power -= 10 DOPO AVER FATTO I LIVELLI
        if(powerCheck && secondTime <= 0){  classParent.gameObject.GetComponent<Player>().power -=  10;powerCheck = false;}//classParent.gameObject.GetComponent<Player>().secondAttackPower;
        if(classParent.GetComponent<Player>().mana < classParent.GetComponent<Player>().startMana){
          classParent.GetComponent<Player>().ManaAttack(-1f/60f);//Recupera 1 al secondo  
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
            anim.Play(classParent.gameObject.GetComponent<Player>().lastClassUsed + "Attack1");
            GameObject arrow;
            if(!facingRight){
                arrow = Instantiate(Resources.Load<GameObject>("Prefabs/Arrow"), new Vector2(transform.position.x - 0.5f - classParent.GetComponent<SpriteRenderer>().sprite.bounds.max.x, transform.position.y), Quaternion.identity);
            }else{
                arrow = Instantiate(Resources.Load<GameObject>("Prefabs/Arrow"), new Vector2(transform.position.x + 0.5f +  classParent.GetComponent<SpriteRenderer>().sprite.bounds.max.x, transform.position.y), Quaternion.identity);
            }
            arrow.GetComponent<ArrowScript>().arrowScript(facingRight, classParent.GetComponent<Player>().power);
            currentTime = classParent.GetComponent<Player>().speedAttack;
        } 
        else if(num == 2 && secondTime <= 0){
            anim.Play(classParent.gameObject.GetComponent<Player>().lastClassUsed + "Attack2"); //SOSTITUISCI IL COMMENTO CON .....power += 10 DOPO AVER FATTO I LIVELLI
            classParent.GetComponent<Player>().ManaAttack(25);//classParent.GetComponent<Player>().secondAttackPower   DA SOSTITUIRE QUANDO HAI FATTO I LIVELLI
            classParent.gameObject.GetComponent<Player>().power +=  10;//classParent.gameObject.GetComponent<Player>().secondAttackPower;
            powerCheck = true;
            secondTime = secondAttackDelay;
        }
    }

    public void ReleaseAttack(){
        if(classParent.gameObject.GetComponent<Player>().lastClassUsed == "Warrior" ) anim.SetBool("Released", true);
    }

}
