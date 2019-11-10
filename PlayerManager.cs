using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<string> playerChosen = new List<string>();

    private void Awake() {
        if(instance == null)instance = this;
    }

    public void AddPlayerJoined(string playerName){
        playerChosen.Add(playerName);
    }

    public void RemovePlayerJoined(string playerName){
        playerChosen.Remove(playerName);
    }

    public bool CheckPlayer(string playerName){
        for(int i = 0; i < playerChosen.Count; i++){
            if(playerName == playerChosen[i]){
                return true;
            }
        }
        return false;
    }

    public List<string> GetUnlockedHuman(string unlocked){
        List<string> toReturn = new List<string>();
        string[] text = unlocked.Split(';');
        for(int i = 0; i < text.Length; i++){
            toReturn.Add(text[i]);
        }
        return toReturn;
    }
}
