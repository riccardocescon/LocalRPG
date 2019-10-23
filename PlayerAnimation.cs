using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;

    public float moveInput = 0f;
    private Animator anim;
    private float distToGround;


    private void Start() {
        anim = GetComponent<Animator>();
        distToGround = GetComponent<Collider2D>().bounds.extents.y;
    }
    private void Awake() {
        if(instance == null)instance = this;
    }

    private void FixedUpdate() {
        Player.instance.rb.velocity = new Vector2(moveInput * Player.instance.speed, Player.instance.rb.velocity.y);
    }

    public void SetSomething(string button, int JoystickNum){
        moveInput = Input.GetAxis(button);
        anim.SetBool("MoveRight", true);
    }
}
