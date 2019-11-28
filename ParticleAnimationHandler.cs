using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAnimationHandler : MonoBehaviour
{
    private void Start() {
        StartCoroutine(RandomTime());
    }

    private IEnumerator RandomTime(){
        while(true){
            int num = Random.Range(8, 12);
            yield return new WaitForSeconds(num);
            ParticleFollowPath.instance.Animate();
        }
        
    }
}
