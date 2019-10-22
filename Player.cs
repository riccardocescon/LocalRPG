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
        
    #endregion

    public float startTimeBtwAttack;
    private float timeBtwAttacks;



    private void Awake() {
        if(instance == null)instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
}
