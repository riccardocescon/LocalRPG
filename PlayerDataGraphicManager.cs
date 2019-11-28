using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataGraphicManager : MonoBehaviour
{
    public static PlayerDataGraphicManager instance;

    public GameObject[] playerPanel = new GameObject[4];
    public GameObject[] playerContainer = new GameObject[4];
    public GameObject[] useSecondAttack = new GameObject[4];
    private int[] playerDefeated = new int[4];
    public GameObject xpDB;

    #region EndPanel
        public GameObject[] endPanel = new GameObject[4];
        public Text[] nameText = new Text[4];
        public Text[] levelText = new Text[4];
        public Text[] expText = new Text[4];
        public Text[] healthText = new Text[4];
        public Text[] powerText = new Text[4];
        public Text[] manaText = new Text[4];
        public Text[] armorText = new Text[4];
        public Text[] speedText = new Text[4];
        public Text[] attackSpeedText = new Text[4];
        public Text[] criticText = new Text[4];
        public Text[] levelUpText = new Text[4];
        public Text[] victoryText = new Text[4];
        public GameObject timer;

        [Header("Unity Stuff")]
        public Image[] expBar = new Image[4];

    #endregion

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
             animator[i] = this.gameObject.transform.GetChild(0).GetChild(5).GetChild(i).GetComponent<Animator>();
        }
        for(int i = 0; i < playerContainer.Length; i++){
            levelUpText[i].gameObject.SetActive(false);
        }

    }

    public void AddPlayer(GameObject pg, PlayerData player){
        playerContainer[playerCont] = pg;
        SetData(player, playerCont);
        playerCont++;
    }

    public void ShowHideSecondAttack(bool show, int num){
        useSecondAttack[num].SetActive(show);
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

    public void Die(int num){
        playerDefeated[dead] = num;
        dead++;
        if(dead == (numPlayer)){
            EndLevelScreen();
        }else{
            
        }
    }

    private void EndLevelScreen(){
        catchButton = true;
        timer.GetComponent<Timer>().Pause();
        for(int i = 0; i < numPlayer + 1; i++){
            endPanel[i].SetActive(true);
            playerContainer[i].GetComponent<Movement>().pause = true;
            animator[i].Play("EndPanel" + (i + 1));
        }
        UpdateExp();
        SetEndLevelData();
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

        for(int i = 0; i < playerCont; i++){
            SaveSystem.SavePlayer(playerContainer[i].GetComponent<Player>());
        }
    }

    private void EndLevelAnimate(){ //IMPOSTA TUTTE LE COSE SUGLI END PANEL
        for(int i = 0; i < numPlayer + 1; i++){
            if(playerContainer[i].GetComponent<Player>().win){
                endPanel[i].transform.GetChild(2).GetComponent<EndAnimation>().Animate(playerContainer[i].GetComponent<Player>().lastClassUsed, i + 1, true);
            }else{
                endPanel[i].transform.GetChild(2).GetComponent<EndAnimation>().Animate(playerContainer[i].GetComponent<Player>().lastClassUsed, i + 1, false);
            }
        }
    }

    private void UpdateExp(){
        for(int i = 0; i < playerContainer.Length; i++){
            if(playerContainer[i] == null) return;

            if(playerContainer[i].GetComponent<Player>().win){
                playerContainer[i].GetComponent<Player>().UpdateXP(20);
            }else{
                playerContainer[i].GetComponent<Player>().UpdateXP(10);
            }

            if(playerContainer[i].GetComponent<Player>().xp >= xpDB.GetComponent<ExpDataBase>().GetMaxExp(playerContainer[i].GetComponent<Player>().lvl)){
                playerContainer[i].GetComponent<Player>().xp = playerContainer[i].GetComponent<Player>().xp % xpDB.GetComponent<ExpDataBase>().GetMaxExp(playerContainer[i].GetComponent<Player>().lvl);
                playerContainer[i].GetComponent<Player>().lvl++;
                playerContainer[i].GetComponent<Player>().leveled = true;
                UpgradeStats(playerContainer[i].GetComponent<Player>());
            }
        }
    }

    private void UpgradeStats(Player pg){
        pg.startHealth += xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(pg, "Health");
        pg.power += xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(pg, "Power");
        pg.startMana += xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(pg, "Mana");
        pg.armor += xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(pg, "Armor");
        pg.speed += xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(pg, "Speed");
        pg.speedAttack += xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(pg, "AttackSpeed");
        pg.critic += xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(pg, "Critic");
    }

    private void SetEndLevelData(){
        for(int i = 0; i < playerCont; i++){
            if(playerContainer[i] == null) return;
            nameText[i].text = playerContainer[i].GetComponent<Player>().name;
            levelText[i].text = "Level : " + playerContainer[i].GetComponent<Player>().lvl;
            levelText[i].gameObject.SetActive(true);
            expText[i].text = playerContainer[i].GetComponent<Player>().xp + "/" + xpDB.GetComponent<ExpDataBase>().GetMaxExp(playerContainer[i].GetComponent<Player>().lvl);
            expBar[i].fillAmount = (playerContainer[i].GetComponent<Player>().xp / (float)xpDB.GetComponent<ExpDataBase>().GetMaxExp(playerContainer[i].GetComponent<Player>().lvl));
            UIChar(i);

            if(playerContainer[i].GetComponent<Player>().win){
                victoryText[i].text = "VICTORY";
            }else{
                victoryText[i].text = "DEFEAT";
            }

            if(playerContainer[i].GetComponent<Player>().leveled){
                levelUpText[i].gameObject.SetActive(true);
            }

            playerContainer[i].GetComponent<Player>().SaveResetedPlayer();

        }
    }

    private void UIChar(int i){
        playerContainer[i].GetComponent<Player>().lvl++;
        if(xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Health") != 0){
            healthText[i].text = "Health :  " + playerContainer[i].GetComponent<Player>().startHealth + "  (+" + xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Health") + ")";
        }else{
            healthText[i].text = "Health :  " + playerContainer[i].GetComponent<Player>().startHealth;
        }

        if(xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Power") != 0){
            powerText[i].text = "Power :  " + playerContainer[i].GetComponent<Player>().power + "  (+" + xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Power") + ")";
        }else{
            powerText[i].text = "Power :  " + playerContainer[i].GetComponent<Player>().power;
        }

        if(xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Mana") != 0){
            manaText[i].text = "Mana :  " + playerContainer[i].GetComponent<Player>().startMana + "  (+" + xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Mana") + ")";
        }else{
            manaText[i].text = "Mana :  " + playerContainer[i].GetComponent<Player>().startMana;
        }

        if(xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Armor") != 0){
            armorText[i].text = "Armor :  " + playerContainer[i].GetComponent<Player>().armor + "  (+" + xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Armor") + ")";
        }else{
            armorText[i].text = "Armor :  " + playerContainer[i].GetComponent<Player>().armor;
        }

        if(xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Speed") != 0){
            speedText[i].text = "Speed :  " + playerContainer[i].GetComponent<Player>().speed + "  (+" + xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Speed")+ ")";
        }else{
            speedText[i].text = "Speed :  " + playerContainer[i].GetComponent<Player>().speed;
        }

        if(xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "AttackSpeed") != 0){
            attackSpeedText[i].text = "AtkSpeed: " + playerContainer[i].GetComponent<Player>().speedAttack + "(+" + xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "AttackSpeed")+ ")";
        }else{
            attackSpeedText[i].text = "AtkSpeed :  " + playerContainer[i].GetComponent<Player>().speedAttack;
        }

        if(xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Critic") != 0){
            criticText[i].text = "Critic% : " + playerContainer[i].GetComponent<Player>().critic + "  (+" + xpDB.GetComponent<ExpDataBase>().GetAmountNextLevel(playerContainer[i].GetComponent<Player>(), "Critic")+ ")";
        }else{
            criticText[i].text = "Critic% :  " + playerContainer[i].GetComponent<Player>().critic;
        }

        playerContainer[i].GetComponent<Player>().lvl--;
    }

}
