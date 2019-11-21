using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public float health;
    public float power;
    public float secondAttackPower;
    public float speed;
    public float speedAttack;
    public int xp;
    public float mana;
    public float armor;
    public string lastClassUsed;
    public string lastRaceUsed;
    public int lvl;
    public string classUnlocked;

    public PlayerData(Player player){
        name = player.name;
        health = player.health;
        power = player.power;
        secondAttackPower = player.secondAttackPower;
        speed = player.speed;
        speedAttack = player.speedAttack;
        xp = player.xp;
        mana = player.mana;
        armor = player.armor;
        lastClassUsed = player.lastClassUsed;
        lastRaceUsed = player.lastRaceUsed;
        lvl = player.lvl;
        classUnlocked = player.classUnlocked;

    }

    public PlayerData(PlayerData player){
        name = player.name;
        health = player.health;
        power = player.power;
        secondAttackPower = player.secondAttackPower;
        speed = player.speed;
        speedAttack = player.speedAttack;
        xp = player.xp;
        mana = player.mana;
        armor = player.armor;
        lastClassUsed = player.lastClassUsed;
        lastRaceUsed = player.lastRaceUsed;
        lvl = player.lvl;
        classUnlocked= player.classUnlocked;

    }


    public PlayerData(string playerName){
        name = playerName;
        health = 100;
        power = 6;
        secondAttackPower = 0;
        mana = 40f;
        armor = 40;
        speed = 5f;
        speedAttack = 0.3f;
        xp = 0;
        lastClassUsed = "";
        lastRaceUsed = "";
        lvl = 1;
        classUnlocked = "Warrior;Ranger;Mage";
    }

    public void SetRace(string race){
        lastRaceUsed = race;
    }

}
