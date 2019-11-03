using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;

    public Animator[] animators;
    public float[] moveInput = new float[4];
    public float[] horizontalForce = new float[4];
    public Player[] playerGameObject;




    private void Awake() {
        if(instance == null) instance = this;
    }

    private void Start() {
        for(int i = 0; i < 4; i++){
            moveInput[i] = 0f;
        }
    }


    public void MoveHorizontal(string button, int JoystickNum){
        moveInput[JoystickNum] = Input.GetAxis(button);
        horizontalForce[JoystickNum] = moveInput[JoystickNum];
    }

    public void MoveVertical(string button, int JoystickNum){
        playerGameObject[JoystickNum].rb.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
    }

}
