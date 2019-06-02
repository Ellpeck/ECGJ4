using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    private static readonly int Out = Animator.StringToHash("FadeOut");
    public static Fade Instance;
    private Animator animator;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        this.animator = this.GetComponent<Animator>();
    }

    public void FadeOut() {
        this.animator.SetTrigger(Out);
    }

}