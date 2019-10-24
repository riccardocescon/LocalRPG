using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;

    public float moveInput = 0f;
    private float horizontalForce;
    private Animator anim;
    private float distToGround;


    private void Start() {
        anim = GetComponent<Animator>();
        distToGround = GetComponent<Collider2D>().bounds.extents.y;
    }
    private void Awake() {
        if(instance == null)instance = this;
    }

    private void Update() {
        if(moveInput > 0){
            anim.SetBool("MoveRight", true);
        }else
        {
            anim.SetBool("MoveRight", false);
        }
    }

    private void FixedUpdate() {
        GetComponent<Player>().rb.velocity = new Vector2(horizontalForce * GetComponent<Player>().speed, GetComponent<Player>().rb.velocity.y);

        if(horizontalForce > 0){
            horizontalForce -=  2 * Time.deltaTime;
        }else if(horizontalForce < 0){
            horizontalForce += 2 * Time.deltaTime;
        }
        
    }

    public void MoveHorizontal(string button){
        moveInput = Input.GetAxis(button);
        horizontalForce = moveInput;
    }

    public void MoveVertical(string button){
        GetComponent<Player>().rb.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
    }

    public void StopMove(){
        moveInput = 0;
    }

    public void Attack(){
        //Attack Animation
        Debug.Log("Attack");
    }
}
