using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
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

    public PlayerData(Player player){
        name = player.name;
        health = player.health;
        power = player.power;
        speed = player.speed;
        speedAttack = player.speedAttack;
        xp = player.xp;
        mana = player.mana;
        armor = player.armor;
        lastClassUsed = player.lastClassUsed;
        lvl = player.lvl;
        classUnlocked = player.classUnlocked;

    }

    public PlayerData(PlayerData player){
        name = player.name;
        health = player.health;
        power = player.power;
        speed = player.speed;
        speedAttack = player.speedAttack;
        xp = player.xp;
        mana = player.mana;
        armor = player.armor;
        lastClassUsed = player.lastClassUsed;
        lvl = player.lvl;
        classUnlocked= player.classUnlocked;

    }

    public PlayerData(string playerName){
        name = playerName;
        health = 0;
        power = 0;
        speed = 0;
        speedAttack = 0;
        xp = 0;
        mana = 0;
        armor = 0;
        lastClassUsed = "";
        lvl = 0;
        classUnlocked = "Guerriero;Arciere;Mago";
    }

}
