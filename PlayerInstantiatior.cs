using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiatior : MonoBehaviour
{
    public int JoystickNum;
    private GameObject pg;
    private PlayerData player;


    // Start is called before the first frame update
    void Start()
    {
        JoystickNum = JoystickHolder.instance.GetJoystickNum();
        player = SaveSystem.GetPlayer(JoystickNum);
        if(!(player == null)){       //Se numero joystick è in gioco
            pg = Instantiate(Resources.Load<GameObject>("Prefabs/Warrior"), JoystickHolder.instance.GetPlayerPosition(JoystickNum), Quaternion.identity);
            pg.SetActive(true);
            GetComponent<Controller>().SetPlayer(pg, JoystickNum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
