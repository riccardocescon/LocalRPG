using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataGraphicManager : MonoBehaviour
{
    public static PlayerDataGraphicManager instance;

    public GameObject[] playerPanel = new GameObject[4];
    public GameObject[] playerContainer = new GameObject[4];
    public GameObject[] endPanel = new GameObject[4];

    private Vector2 startPosition = new Vector2(72, -456);

    private bool first = false;
    public int numPlayer = -1;
    private int playerCont = 0;
    public Animator[] animator = new Animator[4];


    private int dead = 0;
    private bool catchButton = false;


    private void Awake() {
        if(instance == null)instance = this;
    }

    private void Start() {
        for(int i = numPlayer + 1; i < playerPanel.Length; i++){
            playerPanel[i].SetActive(false);
        }
        for(int i = 0; i < endPanel.Length; i++){
            endPanel[i].SetActive(false);
        }
        for(int i = 0; i < 4; i++){
             animator[i] = this.gameObject.transform.GetChild(0).GetChild(4).GetChild(i).GetComponent<Animator>();
        }
    }

    public void AddPlayer(GameObject pg, PlayerData player){
        playerContainer[playerCont] = pg;
        SetData(player, playerCont);
        playerCont++;
    }

    public void SetDataGraphic(PlayerData player){
        numPlayer++;
        SetPosition();
    }
                                      
    private void SetData(PlayerData player, int pos){
        playerPanel[pos].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(player.lastRaceUsed + "/" + player.lastClassUsed + "/" + player.lastClassUsed + "Face");
        
        playerPanel[pos].transform.GetChild(1).GetComponent<Text>().text = playerContainer[pos].GetComponent<Player>().health.ToString();
        playerPanel[pos].transform.GetChild(3).GetComponent<Text>().text = playerContainer[pos].GetComponent<Player>().mana.ToString();
    }

    public void UpdateDataGraphic(int pos, string characteristic){
        switch(characteristic){
            case "health":
                playerPanel[pos].transform.GetChild(1).GetComponent<Text>().text = ((int)(playerContainer[pos].GetComponent<Player>().health)).ToString();
            break;

            case "mana":
                playerPanel[pos].transform.GetChild(3).GetComponent<Text>().text = ((int)(playerContainer[pos].GetComponent<Player>().mana)).ToString();
            break;

            default:
                playerPanel[pos].transform.GetChild(1).GetComponent<Text>().text = ((int)(playerContainer[pos].GetComponent<Player>().health)).ToString();
                playerPanel[pos].transform.GetChild(3).GetComponent<Text>().text = ((int)(playerContainer[pos].GetComponent<Player>().mana)).ToString();
            break;
        }
            
    }
       

    private void SetPosition(){
        switch(numPlayer){
            case 0:
                playerPanel[0].SetActive(true);
            break;

            case 1:
                playerPanel[1].SetActive(true);
                playerPanel[0].transform.localPosition = new Vector2(startPosition.x - 150, startPosition.y);
                playerPanel[1].transform.localPosition = new Vector2(startPosition.x + 150, startPosition.y);
                
            break;

            case 2:
                playerPanel[2].SetActive(true);
                playerPanel[0].transform.localPosition = new Vector2(startPosition.x - 225, startPosition.y);
                playerPanel[1].transform.localPosition = new Vector2(startPosition.x, startPosition.y);
                playerPanel[2].transform.localPosition = new Vector2(startPosition.x + 225, startPosition.y);
            break;

            case 3:
                playerPanel[3].SetActive(true);
                playerPanel[0].transform.localPosition = new Vector2(startPosition.x - 345, startPosition.y);
                playerPanel[1].transform.localPosition = new Vector2(startPosition.x - 125, startPosition.y);
                playerPanel[2].transform.localPosition = new Vector2(startPosition.x + 100, startPosition.y);
                playerPanel[3].transform.localPosition = new Vector2(startPosition.x + 320, startPosition.y);
            break;

        }
    }

    public void Die(){
        dead++;
        if(dead == (numPlayer + 1)){
            EndLevelScreen();
        }else{
            
        }
    }

    private void EndLevelScreen(){
        catchButton = true;
        for(int i = 0; i < numPlayer + 1; i++){
            endPanel[i].SetActive(true);
            playerContainer[i].GetComponent<Movement>().pause = true;
            animator[i].Play("EndPanel" + (i + 1));
        }
        EndLevelAnimate();

        switch(numPlayer){
            case 0:
            break;

            case 1:
                endPanel[0].transform.position = new Vector2(endPanel[0].transform.position.x - 450, endPanel[0].transform.position.y);
                endPanel[1].transform.position = new Vector2(endPanel[1].transform.position.x + 350, endPanel[1].transform.position.y);
            break;

            case 2:
                endPanel[0].transform.position = new Vector2(endPanel[0].transform.position.x - 400, endPanel[0].transform.position.y);
                endPanel[2].transform.position = new Vector2(endPanel[2].transform.position.x + 400, endPanel[2].transform.position.y);
            break;

            case 3:
                endPanel[0].transform.position = new Vector2(endPanel[0].transform.position.x - 600, endPanel[0].transform.position.y);
                endPanel[1].transform.position = new Vector2(endPanel[1].transform.position.x - 200, endPanel[1].transform.position.y);
                endPanel[2].transform.position = new Vector2(endPanel[2].transform.position.x + 200, endPanel[2].transform.position.y);
                endPanel[3].transform.position = new Vector2(endPanel[3].transform.position.x + 600, endPanel[3].transform.position.y);
            break;
        }
    }

    private void EndLevelAnimate(){ //IMPOSTA TUTTE LE COSE SUGLI END PANEL
        for(int i = 0; i < numPlayer + 1; i++){
            if(playerContainer[i].GetComponent<Player>().health > 0){
                endPanel[i].transform.GetChild(2).GetComponent<EndAnimation>().Animate(playerContainer[i].GetComponent<Player>().lastClassUsed, i + 1, true);
            }else{
                endPanel[i].transform.GetChild(2).GetComponent<EndAnimation>().Animate(playerContainer[i].GetComponent<Player>().lastClassUsed, i + 1, false);
            }
        }
    }
}
