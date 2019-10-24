﻿using System.Collections;
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
        
        PlayerData pg = SaveSystem.GetPlayer(this.gameObject.GetComponent<Controller>().JoystickNum);
        if(pg != null){
            name = pg.name;
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
        }else{
            this.gameObject.SetActive(false);
        }

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
