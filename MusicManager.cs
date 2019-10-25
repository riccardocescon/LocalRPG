using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource manager;
    private AudioClip clip;
    
    private void Start() {
        manager = GetComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("Music/Main");
        manager.clip = clip;
        manager.volume = 1;
        manager.Play();
    }
    private void Update() {
        if(!manager.isPlaying){
            manager.Play();
        }
    }
}
