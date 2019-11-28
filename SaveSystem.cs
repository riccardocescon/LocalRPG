using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SavePlayer(Player player){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player" + player.name + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SavePlayer(PlayerData player){
        Player pg = new Player();
        pg.name = player.name;
        pg.health = player.health;
        pg.power = player.power;
        pg.critic = player.critic;
        pg.speed = player.speed;
        pg.speedAttack = player.speedAttack;
        pg.secondAttackPower = player.secondAttackPower;
        pg.allXP = player.xp;
        pg.mana = player.mana;
        pg.armor = player.armor;
        pg.lastClassUsed = player.lastClassUsed;
        pg.lastRaceUsed = player.lastRaceUsed;
        pg.lvl = player.lvl;
        pg.classUnlocked = player.classUnlocked;
        SavePlayer(pg);
    }

    public static PlayerData LoadPlayer(string race, string name){
        string path = "";
        switch(race){
            case "Human":
                path = Application.persistentDataPath + "/player" + name + ".data";
            break;

            case "Monster":
                path = Application.persistentDataPath + "/monster" + name + ".data";
            break;

            case "Draconic":
                path = Application.persistentDataPath + "/draconic" + name + ".data";
            break;
        }
       
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            
            return data;

        }else{
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void DeletePlayer(string name){
        File.Delete(Application.persistentDataPath + "/player" + name + ".data");
        UnityEditor.AssetDatabase.Refresh();
    }

    public static PlayerData GetPlayer(int joystickNum){
        if(joystickNum == -1) return null;
        string data = PlayerPrefs.GetString("GetPlayer" + joystickNum);
        if(data[0] == ';') return null;
        string dataRace = "";
        string dataName = "";
        for(int i = 0; i < data.Length; i++){
            if(data[i].Equals(';')){
                break;
            }else{
                dataRace += data[i];
            }
        }
        for(int i = dataRace.Length + 1; i < data.Length; i++){
            if(data[i].Equals(';')){
                break;
            }else{
                dataName += data[i];
            }
        }
        return LoadPlayer(dataRace, dataName);
    }

    
}
