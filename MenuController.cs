using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public int JoystickNum;
    public GameObject selector;
    private int currentSlot = 0;
    private float startTime = 0.5f;
    private float currentTIme = 0;

    private void Update() {
        if(Input.GetAxis("LeftStickVertical" + JoystickNum) > 0.5f && currentTIme <= 0){
            if(currentSlot < 1){
                selector.transform.position = new Vector2(selector.transform.position.x, selector.transform.position.y - 1.9f);
                currentSlot++;
                currentTIme = startTime;
            }
        }else if(Input.GetAxis("LeftStickVertical" + JoystickNum) < -0.5f && currentTIme <= 0){
            if(currentSlot > 0){
                selector.transform.position = new Vector2(selector.transform.position.x, selector.transform.position.y + 1.9f);
                currentSlot--;
                currentTIme = startTime;
            }

        }else if(Input.GetButtonDown("AButton" + JoystickNum)){
            if(currentSlot == 0){
                SceneManagement.instance.LoadScene("ChosePlayer");
            }else{
                //Open OptionPanel
            }
        }else{
            currentTIme -= Time.deltaTime;
        }
    }
}
