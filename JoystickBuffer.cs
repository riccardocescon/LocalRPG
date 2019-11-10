using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickBuffer : MonoBehaviour
{
    public static JoystickBuffer instance;

    private int[] numbers = new int[4];
    private int cont = -1;
    private int conn = -1;


    private GameObject[] player = new GameObject[4];



    private void Awake() {
        if(instance == null) instance = this;
    }


    private void Start() {
        for(int i = 0; i < 4; i++){
            numbers[i] = i + 1;
        }
    }

    public int GetJoystickNumber(){
        cont++;
        return numbers[cont];
    }

    public bool IsJoystickConnected(){
        if(conn <= cont){
            conn++;
            return true;
        }else{
            conn++;
            return false;
        }
    }




    public void SetJoystickInfo(int _JoystickNum, GameObject _player){
        _JoystickNum--;
        player[_JoystickNum] = _player;
    }

    public GameObject GetPlayer(int joystickNum){
        joystickNum--;
        return player[joystickNum];
    }

}
