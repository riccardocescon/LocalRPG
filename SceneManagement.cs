using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;

    private void Awake() {
        if(instance == null)instance = this;
    }

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }


}
