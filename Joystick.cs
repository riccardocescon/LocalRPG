using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{

    public GameObject PressAToPlayImage;
    public GameObject selector;
    public GameObject[] slot;
    public GameObject AddPlayer;
    public GameObject RaceSeleciton;
    private Vector2 startPosition;
    public int JoystickNum;
    private float startTime = 0.8f;
    private float time;
    private float startDownTime = 0.1f;
    private float downTime;
    private bool joined = false;
    private int currentSlot = 1;
    private int currentPlayerNum;
    private bool raceSelection = false;
    private bool classSelection = false;
    private string raceChosen;
    private PlayerData player;
    private string name;
    private bool play = true;
    private int rotation = 0;
    private string playerRace;

    public bool ready = false;




    // Start is called before the first frame update
    void Start()
    {
        //SetFIrstGame
        //PlayerPrefs.SetInt("FirstGame", 10);
        if(PlayerPrefs.GetInt("FirstGame") != 1){
            PlayerPrefs.SetString("Player0", "-NEW-");
            for(int i = 1; i < 9; i++){
                PlayerPrefs.SetString("Player" + i, "");
            }
            PlayerPrefs.SetInt("FirstGame", 1);
        }

        currentPlayerNum = 0;
        time = startTime;
        downTime = startDownTime;
        selector.SetActive(false);
        AddPlayer.SetActive(false);
        RaceSeleciton.SetActive(false);
        for(int i = 0; i < slot.Length; i++){
            slot[i].SetActive(false);
        }
        startPosition = selector.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(play){
            if(!joined && time <= 0){
            ImageAnimation();
            time = startTime;
            }else{
                time -= Time.deltaTime;
            }

            if(!joined && Input.GetButtonDown("AButton" + JoystickNum)){
                Joined(true);
                downTime = startDownTime;
            }
            
            if(raceSelection){
                if(Input.GetAxis("LeftStickVertical" + JoystickNum) > 0.7f && downTime <= 0 && currentPlayerNum < 2){   // = num Max Race 
                    Down();
                    downTime = startDownTime;
                }else if(Input.GetAxis("LeftStickVertical" + JoystickNum) < -0.7f && downTime <= 0){
                    Up();
                    downTime = startDownTime;
                }else if(Input.GetButtonDown("AButton" + JoystickNum) && downTime <= 0){
                    playerRace = slot[currentSlot - 1].GetComponent<Text>().text;
                    ClassSelectionFunction(playerRace);
                    downTime = startDownTime;
                }else{
                    downTime -= Time.deltaTime;
                }
            }else if(joined){
            if(Input.GetButtonDown("BButton" + JoystickNum)){
                    Exit();
                }
                if(Input.GetAxis("LeftStickVertical" + JoystickNum) > 0.7f && downTime <= 0){
                    Down();
                    downTime = startDownTime;
                }else if(Input.GetAxis("LeftStickVertical" + JoystickNum) < -0.7f && downTime <= 0){
                    Up();
                    downTime = startDownTime;
                }else if(Input.GetButtonDown("AButton" + JoystickNum) && currentPlayerNum == 0 && downTime <= 0 && !PlayerFull()){  //New Player
                    AddPlayer.SetActive(true);
                    for(int i = 0; i < slot.Length; i++){
                        slot[i].SetActive(false);
                    }
                    selector.SetActive(false);
                    downTime = startDownTime;
                }else if(Input.GetButtonDown("AButton" + JoystickNum) && PlayerPrefs.GetString("Player" + currentPlayerNum) != "" && downTime <= 0){
                    if(!CheckPlayerChosen("Player" + currentPlayerNum))
                        PlayerChosen("Player" + currentPlayerNum);
                        downTime = startDownTime;
                }else if(Input.GetButtonDown("XButton" + JoystickNum) && downTime <= 0 && currentPlayerNum != 0){
                    DeletePlayer();
                }else{
                    downTime -= Time.deltaTime;
                }
                
            }

        }else{  //ClassSelection
            if(ready){


            }else{
                if(Input.GetAxis("LeftStickHorizontal" + JoystickNum) > 0.7f && downTime <= 0){
                    rotation++;
                    ClassManager.instance.MoveClassGraphic('d', JoystickNum - 1, rotation, true);
                        //Debug.Log("RightMove");
                    downTime = startDownTime;
                }else if(Input.GetAxis("LeftStickHorizontal" + JoystickNum) < -0.7f && downTime <= 0){
                    rotation--;
                    ClassManager.instance.MoveClassGraphic('s', JoystickNum - 1, rotation, false);
                    // Debug.Log("LeftMove");
                    downTime = startDownTime;

                }else if(Input.GetButtonDown("AButton" + JoystickNum) && downTime <= 0){
                    player = new PlayerData(LoadPlayer(playerRace));
                    ClassManager.instance.ClassSelected(player, JoystickNum, rotation);
                    SaveSystem.SavePlayer(player);
                        //Debug.Log("Pick");
                    downTime = startDownTime;
                    ready = true;
                    ClassManager.instance.SetReady(player, JoystickNum - 1, raceChosen);
                }else{
                    downTime -= Time.deltaTime/5;
                    
                }
            }

        }

    }

    private void ClassSelectionFunction(string race){   //All Begin From Here
        classSelection = true;
        raceChosen = race;
        player = new PlayerData(LoadPlayer(race));
        if(race == "Human"){
           player.SetRace("Humanoid"); 
        }else{
             player.SetRace(race); 
        }
        SaveSystem.SavePlayer(player);
        player = new PlayerData(LoadPlayer(race));
        HideEveryPanel();
        play = false;
        //ClassSelection.instance.StartClassSelection(player[JoystickNum - 1], JoystickNum, race);
        ClassManager.instance.ShowClasses(player, JoystickNum, race);
    }

    private void HideEveryPanel(){
        selector.SetActive(false);
        AddPlayer.SetActive(false);
        RaceSeleciton.SetActive(false);
        for(int i = 0; i < slot.Length; i++){
            slot[i].SetActive(false);
        }
    }

    private void PlayerChosen(string playerString){
        //Debug.Log("Player : " + JoystickNum + " has chosen : " + PlayerPrefs.GetString(playerString));
        PlayerManager.instance.AddPlayerJoined(playerString);
        name = PlayerPrefs.GetString(playerString);

        //Load Player Data
        
        AddPlayer.SetActive(false);
        RaceSeleciton.SetActive(true);
        currentPlayerNum = 0;
        selector.transform.position = startPosition;
        currentSlot = 1;
        raceSelection = true;
        SetRaceSelection();
    }

    private void SetRaceSelection(){
        slot[0].GetComponent<Text>().text = "Human";
        slot[1].GetComponent<Text>().text = "Monster";
        slot[2].GetComponent<Text>().text = "Draconic";
        
    }

    private bool CheckPlayerChosen(string playerString){
        return PlayerManager.instance.CheckPlayer(playerString);
    }

    private bool PlayerFull(){
        for(int i = 1; i < 9; i++){
            if(PlayerPrefs.GetString("Player" + i) == ""){
               return false; 
            } 
        }
        return true;
    }

    private void ImageAnimation(){
        PressAToPlayImage.SetActive(!PressAToPlayImage.activeSelf);
    }

    private void Joined(bool newjoystick){
        joined = true;
        if(newjoystick)ClassManager.instance.JoystickLoaded();
        for(int i = 0; i < slot.Length; i++){
            slot[i].SetActive(true);
        }
        selector.SetActive(true);
        PressAToPlayImage.SetActive(false);
        AddPlayer.SetActive(false);

        UpdatePlayerNameUi();
        
    }

    private void UpdatePlayerNameUi(){  //Bug Grafico
        //Set the names on slots
        for(int i = 0; i < 3; i++){
            if(PlayerPrefs.GetString("Player" + i) != ""){
                slot[i].GetComponent<Text>().text = PlayerPrefs.GetString("Player" + i);
            }
        }
    }

    
    public void Clear(){
        PlayerPrefs.SetString("Player0", "-NEW-");
        for(int i = 1; i < 10; i++){
            SaveSystem.DeletePlayer(PlayerPrefs.GetString("Player" + i));  //Non Va
            PlayerPrefs.SetString("Player" + i, "");
        }
        UpdatePlayerNameUi();
    }


    private void Exit(){
        joined = false;
        ClassManager.instance.JoystickDisconnected();
        for(int i = 0; i < slot.Length; i++){
            slot[i].SetActive(false);
        }
        selector.SetActive(false);
        AddPlayer.SetActive(false);
    }

    private void Down(){
        if(currentSlot < 3){
            selector.transform.position = new Vector2(selector.transform.position.x, selector.transform.position.y - 124.4f);
            currentSlot++;
            currentPlayerNum++;
        }else if(currentPlayerNum < 10 && PlayerPrefs.GetString("Player" + currentPlayerNum) != ""){
            //Controlla se esistono altri personaggi, e in caso scala la lista in giù di uno
            for(int i = 0; i < 3; i++){
                slot[i].GetComponent<Text>().text = PlayerPrefs.GetString("Player" + (currentPlayerNum - (1 - i)));  //Devo spostare i nomi di uno in sopra
            }
            currentPlayerNum++;
        }
    }

    private void Up(){
        if(currentSlot > 1){
            selector.transform.position = new Vector2(selector.transform.position.x, selector.transform.position.y + 124.4f);
            currentSlot--;
            currentPlayerNum--;
        }else if(currentPlayerNum > 0 && PlayerPrefs.GetString("Player" + (currentPlayerNum - 1)) != ""){
            //controlla se esistono altri personaggi, e in caso scala la lista in su di uno
            for(int i = 0; i < 3; i++){
                slot[i].GetComponent<Text>().text = PlayerPrefs.GetString("Player" + (currentPlayerNum - (1 - i)));
            }
            currentPlayerNum--;
        }
    }

    public void SaveCreatedPlayer(InputField inp){
        //Update PlayerPrefs with new Player
        bool equal = false;
        for(int i = 1; i < 10; i++){
            if(PlayerPrefs.GetString("Player" + i) == inp.text){    //Check Name isn't already exsisting
                equal = true;
                break;
            }
            if(PlayerPrefs.GetString("Player" + i) == "" && !equal){
                PlayerPrefs.SetString("Player" + i, inp.text);
                AddPlayer.SetActive(false);

                //Create new Player Data
                CreatePlayer(inp.text);

                //Create new Monster Data


                Joined(false);
                break;
            }
        }
    }

    private void CreatePlayer(string playerName){
        PlayerData pg = new PlayerData(playerName);
        SaveSystem.SavePlayer(pg);
    }

    private PlayerData LoadPlayer(string race){
        return SaveSystem.LoadPlayer(race, name);
    }

    public void DeletePlayer(){
        SaveSystem.DeletePlayer(PlayerPrefs.GetString("Player" + currentPlayerNum));
        PlayerPrefs.SetString("Player" + currentPlayerNum, "");
        Compact(currentPlayerNum);
        if(currentPlayerNum != 1)currentPlayerNum--;
    }

    private void Compact(int num){
        for(int i = num; i < 9; i++){
            PlayerPrefs.SetString("Player" + i, PlayerPrefs.GetString("Player" + (i + 1)));
        }
        UpdatePlayerNameUi();
    }

}
