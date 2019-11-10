using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickHolder : MonoBehaviour
{
    public static JoystickHolder instance;

    public int JoystickNum = 0;

    public GameObject[] player;





    private void Awake() {
        if(instance == null)instance = this;
        
    }

    public int GetJoystickNum(){
        JoystickNum++;
        return JoystickNum;
    }

    public Vector2 GetPlayerPosition(int _JoystickNum){
        _JoystickNum--;
        return player[_JoystickNum].transform.position;
    }



}
