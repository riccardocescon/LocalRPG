using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDataBase : MonoBehaviour
{
    public static ExpDataBase instance;
    private int[] xp = new int[10];


    private void Awake() {
        if(instance == null)instance = this;

        xp[0] = 100;
        xp[1] = 230;
        xp[2] = 350;
        xp[3] = 480;
        xp[4] = 600;
        xp[5] = 720;
        xp[6] = 840;
        xp[7] = 960;
        xp[8] = 1080;
        xp[9] = 1100;
    }

    public int GetMaxExp(int level){
        return xp[level - 1];
    }

    public float GetAmountNextLevel(Player player, string characteristic){
        switch(player.lastClassUsed){
            case "Warrior":
                #region Warrior_Characteristic
                switch(characteristic){
                    case "Health":
                        switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 30;

                            case 2:
                            return 20;

                            case 3:
                            return 30;

                            case 4:
                            return 40;

                            case 5:
                            return 30;
                            
                            case 6:
                            return 50;

                            case 7:
                            return 40;

                            case 8:
                            return 50;

                            case 9:
                            return 70;

                            default:
                            return -2;

                        }

                    case "Power":
                    switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 2;

                            case 2:
                            return 1;

                            case 3:
                            return 1;

                            case 4:
                            return 2;

                            case 5:
                            return 0;
                            
                            case 6:
                            return 2;

                            case 7:
                            return 1;

                            case 8:
                            return 1;

                            case 9:
                            return 3;

                            default:
                            return -2;

                        }

                    case "Mana":
                    switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 5;

                            case 2:
                            return 5;

                            case 3:
                            return 0;

                            case 4:
                            return 10;
                            
                            case 5:
                            return 5;

                            case 6:
                            return 0;

                            case 7:
                            return 5;

                            case 8:
                            return 0;

                            case 9:
                            return 0;

                            default:
                            return -2;

                        }

                    case "Armor":

                    switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 1;

                            case 2:
                            return 0;

                            case 3:
                            return 0;

                            case 4:
                            return 1;
                            
                            case 5:
                            return 0;

                            case 6:
                            return 1;

                            case 7:
                            return 0;

                            case 8:
                            return 1;

                            case 9:
                            return 0;

                            default:
                            return -2;

                        }

                    case "Speed":
                    switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 0;

                            case 2:
                            return 1;

                            case 3:
                            return 0;

                            case 4:
                            return 1;
                            
                            case 5:
                            return 0;

                            case 6:
                            return 0;

                            case 7:
                            return 1;

                            case 8:
                            return 0;

                            case 9:
                            return 0;

                            default:
                            return -2;

                        }

                    case "AttackSpeed":
                    switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 0.2f;

                            case 2:
                            return 0.3f;

                            case 3:
                            return 0;

                            case 4:
                            return 0.3f;
                            
                            case 5:
                            return 0;

                            case 6:
                            return 0.2f;

                            case 7:
                            return 0;

                            case 8:
                            return 0;

                            case 9:
                            return 0.3f;

                            default:
                            return -2;

                        }

                    case "Critic":
                        switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 2;

                            case 2:
                            return 1;

                            case 3:
                            return 1;

                            case 4:
                            return 3;
                            
                            case 5:
                            return 0;

                            case 6:
                            return 0;

                            case 7:
                            return 1;

                            case 8:
                            return 0;

                            case 9:
                            return 1;

                            default:
                            return 1;

                        }

                    default:
                    return -3;
                }
                #endregion

            case "Ranger":
                #region Ranger_Characteristic
                switch(characteristic){
                    case "Health":
                        switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 20;

                            case 2:
                            return 10;

                            case 3:
                            return 15;

                            case 4:
                            return 20;

                            case 5:
                            return 10;
                            
                            case 6:
                            return 25;

                            case 7:
                            return 25;

                            case 8:
                            return 30;

                            case 9:
                            return 35;

                            default:
                            return -2;

                        }

                    case "Power":

                    switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 1;

                            case 2:
                            return 0;

                            case 3:
                            return 1;

                            case 4:
                            return 0;
                            
                            case 5:
                            return 1;

                            case 6:
                            return 1;

                            case 7:
                            return 0;

                            case 8:
                            return 2;

                            case 9:
                            return 0;

                            default:
                            return -2;

                        }

                    case "Mana":

                    switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 10;

                            case 2:
                            return 5;

                            case 3:
                            return 10;

                            case 4:
                            return 10;
                            
                            case 5:
                            return 15;

                            case 6:
                            return 0;

                            case 7:
                            return 5;

                            case 8:
                            return 0;

                            case 9:
                            return 10;

                            default:
                            return -2;

                        }

                    case "Armor":

                    switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 0;

                            case 2:
                            return 1;

                            case 3:
                            return 1;

                            case 4:
                            return 0;
                            
                            case 5:
                            return 0;

                            case 6:
                            return 0;

                            case 7:
                            return 0;

                            case 8:
                            return 1;

                            case 9:
                            return 0;

                            default:
                            return -2;

                        }

                    case "Speed":
                    switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 1;

                            case 2:
                            return 2;

                            case 3:
                            return 1;

                            case 4:
                            return 2;
                            
                            case 5:
                            return 1;

                            case 6:
                            return 1;

                            case 7:
                            return 2;

                            case 8:
                            return 0;

                            case 9:
                            return 1;

                            default:
                            return -2;

                        }

                    case "AttackSpeed":
                    switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 0.3f;

                            case 2:
                            return 0.4f;

                            case 3:
                            return 0.2f;

                            case 4:
                            return 0.3f;
                            
                            case 5:
                            return 0.1f;

                            case 6:
                            return 0.2f;

                            case 7:
                            return 0.1f;

                            case 8:
                            return 0.2f;

                            case 9:
                            return 0.3f;

                            default:
                            return -2;

                        }

                    case "Critic":
                        switch(player.lvl){
                            case 0:
                            return 0;

                            case 1:
                            return 2;

                            case 2:
                            return 1;

                            case 3:
                            return 3;

                            case 4:
                            return 2;
                            
                            case 5:
                            return 0;

                            case 6:
                            return 1;

                            case 7:
                            return 2;

                            case 8:
                            return 1;

                            case 9:
                            return 2;

                            default:
                            return 1;

                        }

                    default:
                    return -3;
                }
                #endregion

            case "Mage":
                return -1;

            default:
            return -1;

        }
    }

}
