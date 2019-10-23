using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    #region Player Data
        public string name;
        public float health;
        public float power;
        public float speed;
        public float speedAttack;
        public int xp;
        public int mana;
        public float armor;
        public string lastClassUsed;
        public int lvl;
        public string classUnlocked;
        public int JoystickNum;
        
    #endregion

    #region PhysicsVariable
        public Rigidbody2D rb;

    #endregion

    #region Time_Variable
        public float startTimeBtwAttack;
        private float timeBtwAttacks;
    #endregion



    private void Awake() {
        if(instance == null)instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        PlayerData pg = SaveSystem.GetPlayer(1);    //1 dovrebbe essere il numero del joystick. Trova il modo per riconoscere che joystick sta usando il giocatore
        name = pg.name;                             //prova a loaddarli 1 all volta dal primo, e dopo averlo loaddato carichi il giocatore, in questo modo dovrebbe andare
        health = pg.health;
        power = pg.power;
        speed = pg.speed;
        speedAttack = pg.speedAttack;
        xp = pg.xp;
        mana = pg.mana;
        armor = pg.armor;
        lastClassUsed = pg.lastClassUsed;
        lvl = pg.lvl;
        classUnlocked = pg.classUnlocked;

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(){
        if(timeBtwAttacks <= 0){
            Debug.Log("Attack");
                //Animation
                //Togli vita al bersagio
            timeBtwAttacks = startTimeBtwAttack;
        }else{
            timeBtwAttacks -= Time.deltaTime;
        }
    }

    private void SetPlayer(){
        
    }
}
