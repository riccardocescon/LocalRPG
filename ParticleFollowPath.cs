using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPath : MonoBehaviour
{
    public static ParticleFollowPath instance;
    public string pathName;
    public float time;

    private void Awake() {
        if(instance == null)instance = this;
    }

    public void Animate(){
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "easetype", iTween.EaseType.easeInOutSine, "time", time));
        //StartCoroutine(Reset());
    }

    private IEnumerator Reset(){
        yield return new WaitForSeconds(8);
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPathReversed(pathName), "easetype", iTween.EaseType.easeInOutSine, "time", 0.1f));
    }

}
