using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public GameObject animationHandler;

    private void OnCollisionEnter2D(Collision2D other) {
        if(animationHandler.GetComponent<AnimationHandler>().attacking && !animationHandler.GetComponent<AnimationHandler>().inflict){
            if(other.gameObject.tag == "Hero"){
                other.gameObject.GetComponent<Player>().TakeDamage(animationHandler.transform.parent.gameObject.GetComponent<Player>());
                animationHandler.GetComponent<AnimationHandler>().inflict = true;
            }
        }
    }

}
