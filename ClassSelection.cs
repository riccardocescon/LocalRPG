using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelection : MonoBehaviour
{
    public static ClassSelection instance;
    public GameObject[] ClassPanel;
    public Text healthText;
    public Text manaText;
    public Text powerText;
    public Text armorText;
    public Text speedText;
    public Text attackSpeedText;
    public Text levelText;
    public Text xpText;
    private List<string> unlockedClass = new List<string>();
    private List<GameObject> allHumanClasses = new List<GameObject>();
    private List<GameObject> DisponibileClasses = new List<GameObject>();
    public GameObject ClassHolder;
    private int contatore = 0;

    private PlayerData[] player = new PlayerData[4];
    private int Joystick;

    private Vector2 newPosition;
    private int rotation = 0;
    private bool joystickReleased = true;
    private Vector2 classesCentre;

    private void Awake() {
        if(instance == null) instance = this;
    }


    void Start()
    {
        for(int i = 0; i < ClassPanel.Length; i++){
            ClassPanel[i].SetActive(false);
        }

        LoadAllHumanClasses();
        for(int i = 0; i < allHumanClasses.Count; i++){
            allHumanClasses[i].SetActive(false);
        }
        
    }

    private void Update() {
        if(Joystick != 0 && ClassPanel[Joystick - 1].activeSelf){
            if(Input.GetAxis("LeftStickHorizontal" + Joystick) > 0.5f){
                MoveClassGraphic('d');
            }else if(Input.GetAxis("LeftStickHorizontal" + Joystick) < -0.5f){
                MoveClassGraphic('s');
            }else if(Input.GetAxis("LeftStickHorizontal" + Joystick) < 0.5f && Input.GetAxis("LeftStickHorizontal" + Joystick) > -0.5f){
                joystickReleased = true;
            }
        }else{
        }
    }

    private void LoadAllHumanClasses(){
        allHumanClasses.Add(ClassHolder.transform.Find("Warrior1").gameObject);
        allHumanClasses.Add(ClassHolder.transform.Find("Ranger1").gameObject);
        allHumanClasses.Add(ClassHolder.transform.Find("Mage1").gameObject);

        allHumanClasses.Add(ClassHolder.transform.Find("Warrior2").gameObject);
        allHumanClasses.Add(ClassHolder.transform.Find("Ranger2").gameObject);
        allHumanClasses.Add(ClassHolder.transform.Find("Mage2").gameObject);

        allHumanClasses.Add(ClassHolder.transform.Find("Warrior3").gameObject);
        allHumanClasses.Add(ClassHolder.transform.Find("Ranger3").gameObject);
        allHumanClasses.Add(ClassHolder.transform.Find("Mage3").gameObject);

        allHumanClasses.Add(ClassHolder.transform.Find("Warrior4").gameObject);
        allHumanClasses.Add(ClassHolder.transform.Find("Ranger4").gameObject);
        allHumanClasses.Add(ClassHolder.transform.Find("Mage4").gameObject);
    }

    private void LoadClasses(string race){
        switch(race){
            case "Human":
            break;

            case "Monster":
            break;

            case "Draconic":
            break;

            case "Ogre":
            break;

            case "Elf":
            break;

            case "Mire":
            break;

            default:
            Debug.Log("DEFA");
            break;
        }
    }

    public void StartClassSelection(PlayerData pg, int Jsn, string race){
        ClassPanel[Jsn - 1].SetActive(true);
        SetVisibile(false);
        Joystick = Jsn;
        player[Joystick - 1] = new PlayerData(pg);
        //LoadClasses(race);
        SetCentre();
        SetData(pg);
    }

    private void SetCentre(){
        switch(Joystick - 1){
            case 0:
                classesCentre = new Vector2(0,0);
            break;

            case 1:
                classesCentre = new Vector2(allHumanClasses[(Joystick - 1) * 3].transform.position.x * 0.55f, allHumanClasses[(Joystick - 1) * 3].transform.position.y * 0);
            break;

            case 2:
                classesCentre = new Vector2(allHumanClasses[(Joystick - 1) * 3].transform.position.x * 0, allHumanClasses[(Joystick - 1) * 3].transform.position.y * -1.55f);
            break;

            case 3:
                classesCentre = new Vector2(allHumanClasses[(Joystick - 1) * 3].transform.position.x * 0.55f, allHumanClasses[(Joystick - 1) * 3].transform.position.y * -1.55f);
            break;
            
        }
    }

    private void SetData(PlayerData pg){
        healthText.text = "health : " + pg.health.ToString();
        manaText.text = "mana : " + pg.mana.ToString();
        powerText.text = "power : " + pg.power.ToString();
        armorText.text = "armor : " + pg.armor.ToString();
        speedText.text = "speed : " + pg.speed.ToString();
        attackSpeedText.text = "attack speed : " + pg.speedAttack.ToString();
        levelText.text = "level : " + pg.lvl.ToString();
        xpText.text = "xp : " + pg.xp.ToString();
        //pg.classUnlocked += ";Ad;dfd";  //////////Da rimuovere: Aggiunge la 4 classe    RICORDA DI AGGIUNGERE DALL'INSPECTOR I NUOVI OMINI
        unlockedClass = PlayerManager.instance.GetUnlockedHuman(pg.classUnlocked);
        SetVisibile(true);
        SetClassGraphic();
        
    }

    private void SetVisibile(bool visible){
        healthText.gameObject.SetActive(visible);
        manaText.gameObject.SetActive(visible);
        powerText.gameObject.SetActive(visible);
        armorText.gameObject.SetActive(visible);
        speedText.gameObject.SetActive(visible);
        attackSpeedText.gameObject.SetActive(visible);
        levelText.gameObject.SetActive(visible);
        xpText.gameObject.SetActive(visible);

        SetDisponibileClasses(visible);
    }

    private void SetDisponibileClasses(bool visible){
        if(!visible) return;
        int start = (Joystick - 1) * 3;
        for(int i = start; i < unlockedClass.Count + start; i++){   //AGGIUNGERE QUI OGNI VOLTA CHE AGGIUNGO UN NUOVO OMINO SULLA PARTE GRAFICA
            switch(unlockedClass[i - start]){
                case "Guerriero":
                    DisponibileClasses.Add(allHumanClasses[(Joystick - 1) * 3 + 0]);
                break;

                case "Arciere":
                    DisponibileClasses.Add(allHumanClasses[(Joystick - 1) * 3 + 1]);
                break;

                case "Mago":
                    DisponibileClasses.Add(allHumanClasses[(Joystick - 1) * 3 + 2]);
                break;

                case "Ad":
                    DisponibileClasses.Add(allHumanClasses[(Joystick - 1) * 3 + 3]);
                break;

                default:
                    DisponibileClasses.Add(allHumanClasses[(Joystick - 1) * 3 + 4]);
                break;

            }
        }
        for(int i = 0; i < DisponibileClasses.Count; i++)   {
            //Debug.Log(DisponibileClasses[i]);   //IN TEORIA CON IL CODICE SOPRA AGGIUNGO LE CLASSI SU QUELLI GIA LOADDATI, GUARDA QUESTO DEBUG E CAPiSCI
        }
    }

    private void SetClassGraphic(){
        int nPunti = unlockedClass.Count;
        float alpha = 360 / nPunti;
        float rad = alpha * Mathf.Deg2Rad;
        float r = 100;
        int start = (Joystick - 1) * 3;


        for(int i = start; i < nPunti + start; i++){
            DisponibileClasses[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2) * r + classesCentre.x, Mathf.Sin(i * rad - Mathf.PI/2) * r + classesCentre.y);
            DisponibileClasses[i].SetActive(true);
        }
    }

    private void MoveClassGraphic(char chose){
        Debug.Log(Input.GetAxis("LeftStickHorizontal" + Joystick) + "    " + Joystick);
        int nPunti = unlockedClass.Count;
        float alpha = 360 / nPunti;
        float rad = alpha * Mathf.Deg2Rad;
        float r = 100;
        int start = (Joystick - 1) * 3;

        if(chose == 'd' && joystickReleased){   //Destra
        joystickReleased = false;
            rotation++;            
            for(int i = start; i < nPunti + start; i++){
                DisponibileClasses[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2 - rad * rotation) * r + classesCentre.x,Mathf.Sin(i * rad - Mathf.PI/2 - rad * rotation) * r + classesCentre.y);
            }

        }else if(chose == 's' && joystickReleased){  //Sinistra
            joystickReleased = false;
            rotation--;
            for(int i = start; i < nPunti + start; i++){
                DisponibileClasses[i].transform.localPosition = new Vector2(Mathf.Cos(i * rad - Mathf.PI/2 -  rad * rotation) * r + classesCentre.x, Mathf.Sin(i * rad - Mathf.PI/2 - rad* rotation) * r + classesCentre.y);
            }
        }
    }

}
