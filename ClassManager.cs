using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassManager : MonoBehaviour
{
    public static ClassManager instance;

    #region UI_Variables
        public GameObject[] classSelectionPanel;
        public Text[] healthText;
        public Text[] manaText;
        public Text[] powerText;
        public Text[] armorText;
        public Text[] speedText;
        public Text[] attackSpeedText;
        public Text[] levelText;
        public Text[] xpText;
        public GameObject[] races;  // 0 = Human, 1 = Monster, 2 = Draconic
        public GameObject[] readyPanel;

    #endregion

    private List<string>[] unlockedClass = new List<string>[4];
    private List<GameObject> DisponibileClasses1 = new List<GameObject>();
    private List<GameObject> DisponibileClasses2 = new List<GameObject>();
    private List<GameObject> DisponibileClasses3 = new List<GameObject>();
    private List<GameObject> DisponibileClasses4 = new List<GameObject>();

    private Vector2 classesCentre1;
    private Vector2 classesCentre2;
    private Vector2 classesCentre3;
    private Vector2 classesCentre4;

    private int joystickLoaded = 0;
    private int playerReady = 0;

    private int[] rot = new int[4];

    private void Start() {
        for(int i = 0; i < races.Length; i++){
            foreach(Transform child in races[i].transform){
                child.gameObject.SetActive(false);
            }  
        }
        

        for(int i = 0; i < 4; i++){
            classSelectionPanel[i].SetActive(false);
            readyPanel[i].SetActive(false);
        }

        for(int i = 0; i < 4; i++){
            rot[i] = -1;
        }

    }


    private void Awake() {
        if(instance == null)instance = this;
    }

    private void MoveSelected(int JoystickNum, bool right){
        if(right)
            rot[JoystickNum]++;
        else
        {
            rot[JoystickNum]--;
        }

        if(rot[JoystickNum] < 0){
            switch(JoystickNum){
                case 0:
                    rot[JoystickNum] = DisponibileClasses1.Count - Mathf.Abs(rot[JoystickNum]);
                break;
                case 1:
                    rot[JoystickNum] = DisponibileClasses2.Count - Mathf.Abs(rot[JoystickNum]);
                break;
                case 2:
                    rot[JoystickNum] = DisponibileClasses3.Count - Mathf.Abs(rot[JoystickNum]);
                break;
                case 3:
                    rot[JoystickNum] = DisponibileClasses4.Count - Mathf.Abs(rot[JoystickNum]);
                break;
            }
        }
        int pos;
        stopAnimation(JoystickNum);
        switch(JoystickNum){
            case 0:
                pos = rot[JoystickNum] % DisponibileClasses1.Count;
                DisponibileClasses1[pos].GetComponent<Animator>().SetBool("Animate", true);
            break;
            case 1:
                pos = rot[JoystickNum] % DisponibileClasses2.Count;
                DisponibileClasses2[pos].GetComponent<Animator>().SetBool("Animate", true);
            break;
            case 2:
                pos = rot[JoystickNum] % DisponibileClasses3.Count;
                DisponibileClasses3[pos].GetComponent<Animator>().SetBool("Animate", true);
            break;
            case 3:
                pos = rot[JoystickNum] % DisponibileClasses4.Count;
                DisponibileClasses4[pos].GetComponent<Animator>().SetBool("Animate", true);
            break;
        }
    }

    private void stopAnimation(int num){
        switch(num){
            case 0:
                for(int i = 0; i < DisponibileClasses1.Count; i++){
                    DisponibileClasses1[i].GetComponent<Animator>().SetBool("Animate", false);
                }  
            break;
            case 1:
                for(int i = 0; i < DisponibileClasses2.Count; i++){
                    DisponibileClasses2[i].GetComponent<Animator>().SetBool("Animate", false);
                }  
            break;
            case 2:
                for(int i = 0; i < DisponibileClasses3.Count; i++){
                    DisponibileClasses3[i].GetComponent<Animator>().SetBool("Animate", false);
                }  
            break;
            case 3:
                for(int i = 0; i < DisponibileClasses4.Count; i++){
                    DisponibileClasses4[i].GetComponent<Animator>().SetBool("Animate", false);
                }  
            break;
        }
    }

    public void ShowClasses(PlayerData pg, int joystickNum, string race){
        int classNum = 0;   //?*
        switch(race){
            case "Human":
                classNum = 0;
            break;

            case "Monster":
                classNum = 1;
            break;

            case "Draconic":
                classNum = 2;
            break;
        }

        joystickNum--;
        classSelectionPanel[joystickNum].SetActive(true);
        healthText[joystickNum].text = "Health : " + pg.health.ToString();
        manaText[joystickNum].text = "Mana : " + pg.mana.ToString();
        powerText[joystickNum].text = "Power : " + pg.power.ToString();
        armorText[joystickNum].text = "Armor : " + pg.armor.ToString();
        speedText[joystickNum].text = "Speed : " + pg.speed.ToString();
        attackSpeedText[joystickNum].text = "Speed Attack : " + pg.speedAttack.ToString();
        levelText[joystickNum].text = "Level : " + pg.lvl.ToString();
        xpText[joystickNum].text = "Exp : " + pg.xp.ToString();

        races[classNum].SetActive(true);

        SetDisponibileCLasses(classNum, joystickNum);

        unlockedClass[joystickNum] = PlayerManager.instance.GetUnlockedHuman(pg.classUnlocked);
        //unlockedClass[joystickNum].Add(";SD");  //4 classe

        SetCentre(joystickNum);

        SetClassGraphic(joystickNum);
    }

    private void SetDisponibileCLasses(int classNum, int joystickNum){
        switch(classNum){
            case 0:
                SetHumanDisponibile(classNum, joystickNum);
            break;

            case 1:
                //SetMonsterDisponibile();
            break;

            case 2:
                //SetDraconicDisponibile();
            break;
        }
        
    }

    private void SetHumanDisponibile(int classNum, int joystickNum){
        switch(joystickNum){
            case 0:
                foreach(Transform child in races[classNum].transform){  //Aggiungi su questo switch per nuove classi e sull'inspector
                    if(child.name == ("Warrior" + (joystickNum + 1))){
                        DisponibileClasses1.Add(child.gameObject);
                    }
                    if(child.name == ("Ranger" + (joystickNum + 1))){
                        DisponibileClasses1.Add(child.gameObject);
                    }
                    if(child.name == ("Mage" + (joystickNum + 1))){
                        DisponibileClasses1.Add(child.gameObject);
                    }
                    if(child.name == ("Tank"+ (joystickNum + 1))){
                        DisponibileClasses1.Add(child.gameObject);
                    }
                }
            break;

            case 1:
                foreach(Transform child in races[classNum].transform){
                    if(child.name == ("Warrior" + (joystickNum + 1))){
                        DisponibileClasses2.Add(child.gameObject);
                    }
                    if(child.name == ("Ranger" + (joystickNum + 1))){
                        DisponibileClasses2.Add(child.gameObject);
                    }
                    if(child.name == ("Mage" + (joystickNum + 1))){
                        DisponibileClasses2.Add(child.gameObject);
                    }
                    if(child.name == ("Tank"+ (joystickNum + 1))){
                        DisponibileClasses2.Add(child.gameObject);
                    }
                }
            break;

            case 2:
                foreach(Transform child in races[classNum].transform){
                    if(child.name == ("Warrior" + (joystickNum + 1))){
                        DisponibileClasses3.Add(child.gameObject);
                    }
                    if(child.name == ("Ranger" + (joystickNum + 1))){
                        DisponibileClasses3.Add(child.gameObject);
                    }
                    if(child.name == ("Mage" + (joystickNum + 1))){
                        DisponibileClasses3.Add(child.gameObject);
                    }
                    if(child.name == ("Tank"+ (joystickNum + 1))){
                        DisponibileClasses3.Add(child.gameObject);
                    }
                }
            break;

            case 3:
                foreach(Transform child in races[classNum].transform){
                    if(child.name == ("Warrior" + (joystickNum + 1))){
                        DisponibileClasses4.Add(child.gameObject);
                    }
                    if(child.name == ("Ranger" + (joystickNum + 1))){
                        DisponibileClasses4.Add(child.gameObject);
                    }
                    if(child.name == ("Mage" + (joystickNum + 1))){
                        DisponibileClasses4.Add(child.gameObject);
                    }
                    if(child.name == ("Tank"+ (joystickNum + 1))){
                        DisponibileClasses4.Add(child.gameObject);
                    }
                }
            break;
        }
    }

    private void SetClassGraphic(int JoystickNum){
        int nPunti = unlockedClass[JoystickNum].Count;
        float alpha = 360 / nPunti;
        float rad = alpha * Mathf.Deg2Rad;
        float r = 100;

        switch(JoystickNum){
            case 0:
                for(int i = 0; i < nPunti; i++){
                    DisponibileClasses1[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2) * r + classesCentre1.x, Mathf.Sin(i * rad - Mathf.PI/2) * r + classesCentre1.y);
                    DisponibileClasses1[i].SetActive(true);
                }
                MoveSelected(JoystickNum, true);
            break;

            case 1:
                for(int i = 0; i < nPunti; i++){
                    DisponibileClasses2[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2) * r + classesCentre2.x, Mathf.Sin(i * rad - Mathf.PI/2) * r + classesCentre2.y);
                    DisponibileClasses2[i].SetActive(true);
                }
                MoveSelected(JoystickNum, true);
            break;

            case 2:
                for(int i = 0; i < nPunti; i++){
                    DisponibileClasses3[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2) * r + classesCentre3.x, Mathf.Sin(i * rad - Mathf.PI/2) * r + classesCentre3.y);
                    DisponibileClasses3[i].SetActive(true);
                }
                MoveSelected(JoystickNum, true);
            break;

            case 3:
                for(int i = 0; i < nPunti; i++){
                    DisponibileClasses4[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2) * r + classesCentre4.x, Mathf.Sin(i * rad - Mathf.PI/2) * r + classesCentre4.y);
                    DisponibileClasses4[i].SetActive(true);
                }
                MoveSelected(JoystickNum, true);
            break;
        }
    }

    public void MoveClassGraphic(char chose, int JoystickNum, int rotation, bool right){
        int nPunti = unlockedClass[JoystickNum].Count;
        float alpha = 360 / nPunti;
        float rad = alpha * Mathf.Deg2Rad;
        float r = 100;
        int start = (JoystickNum) * 3;

        switch(JoystickNum){
            case 0:
                if(chose == 'd'){   //Destra       
                    for(int i = 0; i < nPunti; i++){
                        DisponibileClasses1[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2 - rad * rotation) * r + classesCentre1.x,Mathf.Sin(i * rad - Mathf.PI/2 - rad * rotation) * r + classesCentre1.y);
                    }

                }else if(chose == 's'){  //Sinistra
                    for(int i = 0; i < nPunti; i++){
                        DisponibileClasses1[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2 -  rad * rotation) * r + classesCentre1.x, Mathf.Sin(i * rad - Mathf.PI/2 - rad* rotation) * r + classesCentre1.y);
                    }
                }
            break;

            case 1:
                if(chose == 'd'){   //Destra       
                    for(int i = 0; i < nPunti; i++){
                        DisponibileClasses2[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2 - rad * rotation) * r + classesCentre2.x,Mathf.Sin(i * rad - Mathf.PI/2 - rad * rotation) * r + classesCentre2.y);
                    }

                }else if(chose == 's'){  //Sinistra
                    for(int i = 0; i < nPunti; i++){
                        DisponibileClasses2[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2 -  rad * rotation) * r + classesCentre2.x, Mathf.Sin(i * rad - Mathf.PI/2 - rad* rotation) * r + classesCentre2.y);
                    }
                }
            break;

            case 2:
                if(chose == 'd'){   //Destra       
                    for(int i = 0; i < nPunti; i++){
                        DisponibileClasses3[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2 - rad * rotation) * r + classesCentre3.x,Mathf.Sin(i * rad - Mathf.PI/2 - rad * rotation) * r + classesCentre3.y);
                    }

                }else if(chose == 's'){  //Sinistra
                    for(int i = 0; i < nPunti; i++){
                        DisponibileClasses3[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2 -  rad * rotation) * r + classesCentre3.x, Mathf.Sin(i * rad - Mathf.PI/2 - rad* rotation) * r + classesCentre3.y);
                    }
                }
            break;

            case 3:
                if(chose == 'd'){   //Destra       
                    for(int i = 0; i < nPunti; i++){
                        DisponibileClasses4[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2 - rad * rotation) * r + classesCentre4.x,Mathf.Sin(i * rad - Mathf.PI/2 - rad * rotation) * r + classesCentre4.y);
                    }

                }else if(chose == 's'){  //Sinistra
                    for(int i = 0; i < nPunti; i++){
                        DisponibileClasses4[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2 -  rad * rotation) * r + classesCentre4.x, Mathf.Sin(i * rad - Mathf.PI/2 - rad* rotation) * r + classesCentre4.y);
                    }
                }
            break;
        }

        MoveSelected(JoystickNum, right);
    }

    public void ClassSelected(PlayerData player, int JoystickNum, int rotation){
        string classSelected = "";
        int classNum;
        switch(JoystickNum){
            case 1:
                if(rotation < 0) rotation = DisponibileClasses1.Count - 1;
                classNum = (rotation % DisponibileClasses1.Count) + 1;
                switch(classNum){
                    case 1:
                        classSelected = "Warrior";
                    break;

                    case 2:
                        classSelected = "Ranger";
                    break;

                    case 3:
                        classSelected = "Mage";
                    break;

                    default:
                        Debug.Log(JoystickNum + "; ClassSelectionError");
                    break;
                }
            break;

            case 2:
                if(rotation < 0) rotation = DisponibileClasses2.Count - 1;
                classNum = (rotation % DisponibileClasses2.Count) + 1;
                switch(classNum){
                    case 1:
                        classSelected = "Warrior";
                    break;

                    case 2:
                        classSelected = "Ranger";
                    break;

                    case 3:
                        classSelected = "Mage";
                    break;

                    default:
                        Debug.Log(JoystickNum + "; ClassSelectionError");
                    break;
                }
            break;

            case 3:
                if(rotation < 0) rotation = DisponibileClasses3.Count - 1;
                classNum = (rotation % DisponibileClasses3.Count) + 1;
                switch(classNum){
                   case 1:
                        classSelected = "Warrior";
                    break;

                    case 2:
                        classSelected = "Ranger";
                    break;

                    case 3:
                        classSelected = "Mage";
                    break;

                    default:
                        Debug.Log(JoystickNum + "; ClassSelectionError");
                    break;
                }
            break;

            case 4:
                if(rotation < 0) rotation = DisponibileClasses4.Count - 1;
                classNum = (rotation % DisponibileClasses4.Count) + 1;
                switch(classNum){
                    case 1:
                        classSelected = "Warrior";
                    break;

                    case 2:
                        classSelected = "Ranger";
                    break;

                    case 3:
                        classSelected = "Mage";
                    break;

                    default:
                        Debug.Log(JoystickNum + "; ClassSelectionError");
                    break;
                }
            break;

        }
        

        player.lastClassUsed = classSelected;
    }

    private IEnumerator WaitSec(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

    private void SetCentre(int JoystickNum){
        switch(JoystickNum){
            case 0:
                classesCentre1 = new Vector2(0,0);
            break;

            case 1:
                classesCentre2 = new Vector2(races[(0) * 3].transform.position.x * 1.25f, races[(0) * 3].transform.position.y * 0);
            break;

            case 2:
                classesCentre3 = new Vector2(races[(0) * 3].transform.position.x * 0, races[(0) * 3].transform.position.y * -0.62f);
            break;

            case 3:
                classesCentre4 = new Vector2(races[(0) * 3].transform.position.x * 1.25f, races[(0) * 3].transform.position.y * -0.62f);
            break;
            
        }
    }

    public void JoystickLoaded(){
        joystickLoaded++;
    }

    public void JoystickDisconnected(){
        joystickLoaded--;
    }

    public void SetReady(PlayerData player, int JoystickNum, string playerRace){
        playerReady++;
        PlayerPrefs.SetString("GetPlayer" + (JoystickNum + 1), playerRace + ";" + player.name + ";" + (JoystickNum + 1));
        SetReadyGraphic(player, JoystickNum);
        AllReady();
    }

    public void RemoveReady(int JoystickNum){
        playerReady--;
        readyPanel[JoystickNum].SetActive(false);
    }

    public void AllReady(){
        if(joystickLoaded == playerReady){
            if(joystickLoaded < 4){
                for(int i = 4; i > joystickLoaded; i--){
                     PlayerPrefs.SetString("GetPlayer" + i, "" + ";" + "" + ";" + "");
                }
            }
            SceneManagement.instance.LoadScene("Map1");
        }
    }

    private void SetReadyGraphic(PlayerData player, int JoystickNum){
        readyPanel[JoystickNum].SetActive(true);
        string classChosen = player.lastClassUsed + (JoystickNum + 1);
        switch(JoystickNum){
            case 0:
                for(int i = 0; i < DisponibileClasses1.Count; i++){
                    DisponibileClasses1[i].SetActive(false);
                }
                foreach(GameObject child in DisponibileClasses1){
                    if(classChosen == ("Warrior" + (JoystickNum + 1))){
                        DisponibileClasses1[0].SetActive(true);
                        DisponibileClasses1[0].transform.localPosition = classesCentre1;
                        DisponibileClasses1[0].transform.localScale = new Vector2(3,3);
                        break;
                    }else if(classChosen == ("Ranger" + (JoystickNum + 1))){
                        DisponibileClasses1[1].SetActive(true);
                        DisponibileClasses1[1].transform.localPosition = classesCentre1;
                        DisponibileClasses1[1].transform.localScale = new Vector2(3,3);
                        break;
                    }else if(classChosen == ("Mage" + (JoystickNum + 1))){
                        DisponibileClasses1[2].SetActive(true);
                        DisponibileClasses1[2].transform.localPosition = classesCentre1;
                        DisponibileClasses1[2].transform.localScale = new Vector2(3,3);
                        break;
                    }
                }
            break;

            case 1:
                for(int i = 0; i < DisponibileClasses2.Count; i++){
                    DisponibileClasses2[i].SetActive(false);
                }
                foreach(GameObject child in DisponibileClasses2){
                    if(classChosen == ("Warrior" + (JoystickNum + 1))){
                        DisponibileClasses2[0].SetActive(true);
                        DisponibileClasses2[0].transform.localPosition = classesCentre2;
                        DisponibileClasses2[0].transform.localScale = new Vector2(3,3);
                        break;
                    }else if(classChosen == ("Ranger" + (JoystickNum + 1))){
                        DisponibileClasses2[1].SetActive(true);
                        DisponibileClasses2[1].transform.localPosition = classesCentre2;
                        DisponibileClasses2[1].transform.localScale = new Vector2(3,3);
                        break;
                    }else if(classChosen == ("Mage" + (JoystickNum + 1))){
                        DisponibileClasses2[2].SetActive(true);
                        DisponibileClasses2[2].transform.localPosition = classesCentre2;
                        DisponibileClasses2[2].transform.localScale = new Vector2(3,3);
                        break;
                    }
                }
            break;

            case 2:
                for(int i = 0; i < DisponibileClasses3.Count; i++){
                    DisponibileClasses3[i].SetActive(false);
                }
                foreach(GameObject child in DisponibileClasses2){
                    if(classChosen == ("Warrior" + (JoystickNum + 1))){
                        DisponibileClasses3[0].SetActive(true);
                        DisponibileClasses3[0].transform.localPosition = classesCentre3;
                        DisponibileClasses3[0].transform.localScale = new Vector2(3,3);
                        break;
                    }else if(classChosen == ("Ranger" + (JoystickNum + 1))){
                        DisponibileClasses3[1].SetActive(true);
                        DisponibileClasses3[1].transform.localPosition = classesCentre3;
                        DisponibileClasses3[1].transform.localScale = new Vector2(3,3);
                        break;
                    }else if(classChosen == ("Mage" + (JoystickNum + 1))){
                        DisponibileClasses3[2].SetActive(true);
                        DisponibileClasses3[2].transform.localPosition = classesCentre3;
                        DisponibileClasses3[2].transform.localScale = new Vector2(3,3);
                        break;
                    }
                }
            break;

            case 3:
                for(int i = 0; i < DisponibileClasses4.Count; i++){
                    DisponibileClasses4[i].SetActive(false);
                }
                foreach(GameObject child in DisponibileClasses2){
                    if(classChosen == ("Warrior" + (JoystickNum + 1))){
                        DisponibileClasses4[0].SetActive(true);
                        DisponibileClasses4[0].transform.localPosition = classesCentre4;
                        DisponibileClasses4[0].transform.localScale = new Vector2(3,3);
                        break;
                    }else if(classChosen == ("Ranger" + (JoystickNum + 1))){
                        DisponibileClasses4[1].SetActive(true);
                        DisponibileClasses4[1].transform.localPosition = classesCentre4;
                        DisponibileClasses4[1].transform.localScale = new Vector2(3,3);
                        break;
                    }else if(classChosen == ("Mage" + (JoystickNum + 1))){
                        DisponibileClasses4[2].SetActive(true);
                        DisponibileClasses4[2].transform.localPosition = classesCentre4;
                        DisponibileClasses4[2].transform.localScale = new Vector2(3,3);
                        break;
                    }
                }
            break;
        }
    }
    

}
