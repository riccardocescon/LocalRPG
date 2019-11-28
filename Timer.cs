using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public Text timeText;
    private float time;
    private bool started = false;
    private bool pause = false;

    //set e start

    //pausa

    private void Awake() {
        if(instance == null)instance = this;
    }

    private void Start() {
        time = 60 * 5;
        StartCoroutine(DownTime());
    }

    IEnumerator DownTime(){
        while(time > 0){
            if(!pause){
                time--;
                int min = (int)(time/60);
                int sec = (int)(time % 60);
                if(min <= 0){
                    if(!started && sec <= 20){
                        started = true;
                        StartCoroutine(ChangeColor());
                    }
                    timeText.text = sec.ToString();
                }else{
                    if(sec < 10){
                        timeText.text = min.ToString() + ":0" + sec.ToString();
                    }else{
                        timeText.text = min.ToString() + ":" + sec.ToString();
                    } 
                }
                yield return new WaitForSeconds(1); 
            }else{
                yield return null;
            }
        }
        
        
        //End Time
    }

    IEnumerator ChangeColor(){
        while(time > 0){
            timeText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            timeText.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    public void Pause(){
        pause = true;
    }

    public void Resume(){
        pause = false;
    }
}
