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
    public string xp;
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
        xp = player.allXP;
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
        health = 70;
        power = 16;
        secondAttackPower = 1;
        mana = 70f;
        armor = 9;
        speed = 4f;
        speedAttack = 0.44f;
        xp = "0;0;0;0;0;0;0;0;0;";
        lastClassUsed = "";
        lastRaceUsed = "";
        lvl = 1;
        classUnlocked = "Warrior;Ranger;Mage";
    }

    public void SetRace(string race){
        lastRaceUsed = race;
    }

    public int GetExp(string race, string classChosen){
        switch(race){
            case "Human":   //Warrior; Ranger; Mage; Tank; Sniper; Necromancer; Hunter; Enchanter Rager; Enchaner Warrior;
                switch(classChosen){
                    case "Warrior":
                    return ExtractExp(0);

                    case "Ranger":
                    return ExtractExp(1);

                    case "Mage":
                    return ExtractExp(2);

                    case "Tank":
                    return ExtractExp(3);

                    case "Sniper":
                    return ExtractExp(4);

                    case "Necromancer":
                    return ExtractExp(5);

                    case "Hunter":
                    return ExtractExp(6);

                    case "EnchanterRanger":
                    return ExtractExp(7);

                    case "EnchanterWarrior":
                    return ExtractExp(8);

                    default:
                    return -1;
                }
            default:    //Case player has just been created
            return 0;
        }
    }

    private int ExtractExp(int pos){
        int cont = 0;
        int tempInt = -1;
        for(int i = 0; i < xp.Length; i++){
            if(xp[i].Equals(';')){
                cont++;
                tempInt++;
            }
            tempInt++;
            if(cont == pos)break;
        }
        string temp = "";
        for(int i = tempInt; i < xp.Length; i++){
            if(xp[i].Equals(';'))break;
            temp += xp[i];
        }
        return int.Parse(temp);
    }

    private void SaveExp(int exp, string race, string classChosen){
        switch(race){
            case "Humanoid":
                switch(classChosen){
                    case "Warrior":
                    Overwrite(0, exp);
                    break;

                    case "Ranger":
                    Overwrite(1, exp);
                    break;

                    case "Mage":
                    Overwrite(2, exp);
                    break;
                    
                    case "Tank":
                    Overwrite(3, exp);
                    break;

                    case "Sniper":
                    Overwrite(4, exp);
                    break;
                    
                    case "Necromancer":
                    Overwrite(5, exp);
                    break;
                    
                    case "Hunter":
                    Overwrite(6, exp);
                    break;

                    case "EnchanterRanger":
                    Overwrite(7, exp);
                    break;

                    case "EnchanterWarrior":
                    Overwrite(8, exp);
                    break;

                    default:
                        Debug.Log("ERROR : " + race + " ; " + classChosen);
                    break;
                }
                break;


            default:    //case player has just been created
                xp = "0;0;0;0;0;0;0;0;0;";  //add when uploading new races
            break;
        }
    }

    private void Overwrite(int pos, int amount){

        Debug.Log("Old : " + xp);

        string[] split = xp.Split(char.Parse(";"));
        split[pos] = amount.ToString();
        for(int i = 0; i < split.Length; i++){
            xp = split[i] + ";";
        }
        Debug.Log("New : " + xp);
    }

        /*int contPos = 0;
        int cont = 0;
        string temp = "";
        for(int i = 0; contPos < pos; i++){
            for(int x = i; x < xp.Length; x++){
                temp += xp[x];
                cont++;
                if(xp[x].Equals(';')){
                    contPos++;
                    i = x;
                    break;
                }
            }
        }
        int start = cont;
        for(int i = cont; !xp[i].Equals(';') ; i++){
            cont++;
            temp += xp[i];
        }
        //temp[start] = amount.ToString();
        
        for(int i = cont; i < xp.Length; i++){
            temp += xp[i];
        }

        xp = temp;
    }
    */

}
