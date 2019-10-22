using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    public Rigidbody2D rb;
    private Animator anim;
    private int speed = 3;
    private float moveInput = 0f;
    private float distToGround;


    private void Awake() {
        if(instance == null) instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        anim = GetComponent<Animator>();

        distToGround = GetComponent<Collider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        //#region JoystickLeftStick
            //Jump on ColliderController script
            if(Input.GetAxis("LeftStickHorizontal2") > 0.5f){
            moveInput =Input.GetAxis("LeftStickHorizontal2");
            anim.SetBool("MoveRight", true);
            } 
            else if(Input.GetAxis("LeftStickHorizontal2") < -0.5f){
                moveInput = Input.GetAxis("LeftStickHorizontal2");
                anim.SetBool("MoveRight", false);
            }
            else{
                moveInput = 0f;
                anim.SetBool("MoveRight", false);
            }
        //#endregion
    
        //#region JoystickButtons
            if(Input.GetButtonDown("AButton2")){
                Player.instance.Attack();
            }
        //#endregion
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }


}
