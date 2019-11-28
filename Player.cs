using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public bool instantiated;

    #region Player Data
        public string name;
        public float health;
        public float startHealth;
        public float power;
        public float critic;
        public float secondAttackPower;
        public float speed;
        public float speedAttack;
        public int xp;
        public string allXP;
        public float mana;
        public float startMana;
        public float armor;
        public string lastClassUsed;
        public string lastRaceUsed;
        public int lvl;
        public string classUnlocked;

        public string currentAction;
        public bool win = true;
        public bool leveled = false;


        
    #endregion

    #region PhysicsVariable
        public Rigidbody2D rb;

    #endregion

    #region Time_Variable
        public float startTimeBtwAttack;
        private float timeBtwAttacks;
    #endregion

    #region Particles
        public Transform blood;
        private ParticleSystem.EmissionModule emission;
    #endregion



    private void Awake() {
        if(instance == null)instance = this;
        blood.GetComponent<ParticleSystem>().Play();
        emission  = blood.GetComponent<ParticleSystem>().emission;
        emission.enabled = false;
    }


    public void SetData(){
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        
        PlayerData pg = SaveSystem.GetPlayer(this.gameObject.GetComponent<Movement>().JoystickNum);
        if(pg != null && this.gameObject.GetComponent<Movement>().JoystickNum != -1){
            name = pg.name;
            health = pg.health;
            power = pg.power;
            critic = pg.critic;
            secondAttackPower = pg.secondAttackPower;
            speed = pg.speed;
            speedAttack = pg.speedAttack;
            lastClassUsed = pg.lastClassUsed;
            lastRaceUsed = pg.lastRaceUsed;
            xp = pg.GetExp(lastRaceUsed, lastClassUsed);
            allXP = pg.xp;
            mana = pg.mana;
            armor = pg.armor;
            lvl = pg.lvl;
            classUnlocked = pg.classUnlocked;

            instantiated = true;
        }else{
            this.gameObject.SetActive(false);
            instantiated = false;
        }

        startHealth = health;
        startMana = mana;


        //Load skin
        switch(lastClassUsed){
            case "Warrior":
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Humanoid/Warrior/WarriorFront");
            break;

            case "Ranger":
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Humanoid/Ranger/RangerFront");
            break;

            case "Mage":
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Humanoid/Warrior/WarriorFront");
            break;

        }
        
        PlayerDataGraphicManager.instance.AddPlayer(this.gameObject, SaveSystem.GetPlayer(this.gameObject.GetComponent<Movement>().JoystickNum));
        LoadXP();
    }

    private IEnumerator Particle(){
        emission.enabled = true;
        yield return new WaitForSeconds(0.3f);
        emission.enabled = false;
    }

    public void TakeDamage(Player enemy){
        //SE E' UN COLPO CRITICO USA QUESTO : GameObject.Find("Main Camera").GetComponent<CameraManager>().HitZoom();
        //GameObject.Find("Main Camera").GetComponent<CameraManager>().HitZoom();
        //PlayerDataGraphicManager.instance.PlayerShake(this.gameObject.GetComponent<Movement>().JoystickNum - 1);
        StartCoroutine(Particle());
        float amount = enemy.power;
        if(IsCritic(enemy.critic)){
            float crt = GetCritic(enemy.power);
            amount += crt;
            switch(GetCriticLevel(crt)){
                case 1:
                    //Piccolo movimento dello scermo
                    GameObject.Find("Main Camera").GetComponent<CameraManager>().HitZoom();
                break;

                case 2:
                    //medio movimento dello schermo
                    GameObject.Find("Main Camera").GetComponent<CameraManager>().MediumShake();
                break;

                case 3:
                    //Forte movmento dello schermo
                    GameObject.Find("Main Camera").GetComponent<CameraManager>().CrazyShake();
                break;
            }
        }
        if(currentAction == "Defending"){
            if(amount < armor){
                health--;
            }else{
                if(amount > 2 * armor){
                    Debug.Log(amount - (amount/3) - armor);
                    health = health -  (amount - (amount/3) - armor);
                }else{
                    Debug.Log(amount - (amount/5) - armor);
                    health = health - (amount - (amount/5) - armor);
                }
            }
        }else{
            health = health - (amount - armor);
        }
        this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<ColliderController>().TakeDamage(amount);
        PlayerDataGraphicManager.instance.UpdateDataGraphic(this.gameObject.GetComponent<Movement>().JoystickNum - 1, "health");
        if(health > 0){
            
        }else if(win){
            GameObject.Find("Main Camera").GetComponent<CameraManager>().HitZoom();
            win = false;
            PlayerDataGraphicManager.instance.Die(this.gameObject.GetComponent<Movement>().JoystickNum - 1);
            this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<ColliderController>().Die();
            health = 0;
            //ANIMAZIONE MORTE
        }
        
       
    }

    public void ManaAttack(float amount){
        mana -= amount;
        //if(mana >= 0){
            this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<ColliderController>().UseMana(amount);
            PlayerDataGraphicManager.instance.UpdateDataGraphic(this.gameObject.GetComponent<Movement>().JoystickNum - 1, "mana");
        //}
    }

    public void SetSecondAttackState(bool show){
        GameObject.Find("/PlayersDataGraphicManager").gameObject.GetComponent<PlayerDataGraphicManager>().ShowHideSecondAttack(show, this.gameObject.GetComponent<Movement>().JoystickNum - 1);
    }

    public void SaveResetedPlayer(){
        health = startHealth;
        mana = startMana;
        SaveSystem.SavePlayer(this);
    }

    private void LoadXP(){
        switch(lastRaceUsed){
            case "Humanoid":
                switch(lastClassUsed){
                    case "Warrior":
                    GetXPFromString(0);
                    break;

                    case "Ranger":
                    GetXPFromString(1);
                    break;

                    case "Mage":
                    GetXPFromString(2);
                    break;

                    case "Tank":
                    GetXPFromString(3);
                    break;

                    case "Sniper":
                    GetXPFromString(4);
                    break;

                    case "Necromancer":
                    GetXPFromString(5);
                    break;

                    case "Hunter":
                    GetXPFromString(6);
                    break;

                    case "EnchanterRanger":
                    GetXPFromString(7);
                    break;

                    case "EnchanterWarrior":
                    GetXPFromString(8);
                    break;

                    default:
                    break;
                }
            break;

            default:
                Debug.Log("ERROR ON LoadXP ON PLAYER");
            break;
        }
    }

    private void GetXPFromString(int pos){
        int cont = 0;
        int tempInt = -1;
        for(int i = 0; i < allXP.Length; i++){
            if(allXP[i].Equals(';')){
                cont++;
                tempInt++;
            }
            tempInt++;
            if(cont == pos)break;
        }
        string temp = "";
        for(int i = tempInt; i < allXP.Length; i++){
            if(allXP[i].Equals(';'))break;
            temp += allXP[i];
        }
        xp = int.Parse(temp);
    }

    public void UpdateXP(int amount){
        xp += amount;
        UpdateStringXP(xp);
        SaveSystem.SavePlayer(this);
    }

    private void UpdateStringXP(int amount){
        switch(lastRaceUsed){
            case "Humanoid":
                switch(lastClassUsed){
                    case "Warrior":
                    ModifyStringXP(0, amount);
                    break;

                    case "Ranger":
                    ModifyStringXP(1, amount);
                    break;

                    case "Mage":
                    ModifyStringXP(2, amount);
                    break;

                    case "Tank":
                    ModifyStringXP(3, amount);
                    break;

                    case "Sniper":
                    ModifyStringXP(4, amount);
                    break;

                    case "Necromancer":
                    ModifyStringXP(5, amount);
                    break;

                    case "Hunter":
                    ModifyStringXP(6, amount);
                    break;

                    case "EnchanterRanger":
                    ModifyStringXP(7, amount);
                    break;

                    case "EnchanterWarrior":
                    ModifyStringXP(8, amount);
                    break;

                    default:
                    break;
                }
            break;

            default:
                Debug.Log("ERROR ON UPDATESTRINGXP ON PLAYER");
            break;
        }
    }

    private void ModifyStringXP(int pos, int amount){
        string[] splitted = allXP.Split(char.Parse(";"));
        splitted[pos] = amount.ToString();
        allXP = "";
        for(int i = 0; i < splitted.Length; i++){
            if(splitted[i].Equals(';'))break;   //evita bug aggiunta ; consecutivi alla fine della stringa
            allXP += splitted[i] + ";";
        }
    }

    public bool IsCritic(float crt){
        int num = Random.Range(1, 101);
        if(crt >= num) return true;
        else return false;
    }

    public float GetCritic(float pw){
        return Random.Range(pw/3, pw * 1.5f);
    }

    public int GetCriticLevel(float num){
        if(num <= (11/18)*power){
            return 1;   //Low
        }else if(num >= (22/18)*power){
            return 3;   //CRITIC
        }else{
            return 2;   //Normal
        }
    }
}
