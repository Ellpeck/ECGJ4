using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private static readonly int Powered = Animator.StringToHash("Powered");
    private static readonly int Unpowered = Animator.StringToHash("Unpowered");

    private Animator animator;
    private new Collider2D collider;

    private bool powered;

    private void Start() {
        this.animator = this.GetComponent<Animator>();
        this.collider = this.GetComponent<Collider2D>();
    }

    public void SetPowered(bool powered) {
        if (powered == this.powered)
            return;
        this.powered = powered;
        this.collider.enabled = !powered;
        this.animator.SetTrigger(powered ? Powered : Unpowered);
    }

}