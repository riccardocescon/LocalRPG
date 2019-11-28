using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public static MatchManager instance;

    public GameObject[] pressAnyButtonText = new GameObject[4];
    public GameObject[] readyText = new GameObject[4];
    private List<int> joystickReady = new List<int>();

    private int ready = 0;


    private void Awake() {
        if(instance = null)instance = this;
        for(int i = 0; i < readyText.Length; i++){
            readyText[i].SetActive(false);
        }
    }

    public void Ready(int num, int joystickNum){
        for(int i = 0; i < joystickReady.Count; i++){
            if(joystickNum == joystickReady[i])return;
        }
        joystickReady.Add(joystickNum);
        pressAnyButtonText[num].SetActive(false);
        readyText[num].SetActive(true);
        ready++;
        CheckReady();
    }

    private void CheckReady(){
        if(ready == GameObject.Find("/PlayersDataGraphicManager").GetComponent<PlayerDataGraphicManager>().numPlayer + 1){
            SceneManagement.instance.LoadScene("ChosePlayer");
        }
    }
}
