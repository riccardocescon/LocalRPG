﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void HitZoom(){
        animator.Play("CameraShake");
    }

}
