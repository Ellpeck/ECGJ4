using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    private static readonly int Down = Animator.StringToHash("Down");
    private static readonly int Up = Animator.StringToHash("Up");

    public int requiredHeaviness;
    public float downTime;
    public Collider2D topCollider;
    public Gradient cableOn;
    public Gradient cableOff;
    public Door[] doors;

    private Animator animator;
    private LineRenderer[] cables;
    private float downTimer;

    private void Start() {
        this.animator = this.GetComponent<Animator>();
        this.cables = this.GetComponentsInChildren<LineRenderer>();
    }

    private void Update() {
        if (this.downTimer > 0) {
            this.downTimer -= Time.deltaTime;
            if (this.downTimer <= 0) {
                this.animator.SetTrigger(Up);
                this.topCollider.enabled = true;
                foreach (var cable in this.cables)
                    cable.colorGradient = this.cableOff;
                foreach (var door in this.doors)
                    door.SetPowered(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null && player.heaviness >= this.requiredHeaviness) {
            this.animator.SetTrigger(Down);
            this.topCollider.enabled = false;
            foreach (var cable in this.cables)
                cable.colorGradient = this.cableOn;
            foreach (var door in this.doors)
                door.SetPowered(true);

            this.downTimer = this.downTime;
        }
    }

}