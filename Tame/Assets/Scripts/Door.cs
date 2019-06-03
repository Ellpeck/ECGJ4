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
    private bool open;
    private bool stopFromClosing;

    private void Start() {
        this.animator = this.GetComponent<Animator>();
        this.collider = this.GetComponent<Collider2D>();
    }

    private void Update() {
        if (this.open != this.powered) {
            if (!this.powered && this.stopFromClosing)
                return;

            this.open = this.powered;
            this.collider.enabled = !this.open;
            this.animator.SetTrigger(this.open ? Powered : Unpowered);
        }
    }

    public void SetPowered(bool powered) {
        this.powered = powered;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
            this.stopFromClosing = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
            this.stopFromClosing = false;
    }

}