using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    private static readonly int Hurt = Animator.StringToHash("Hurt");

    public float maxHealth;
    public float currHealth;
    public float cooldown;

    private float cooldownTimer;
    private Animator animator;

    private void Start() {
        this.currHealth = this.maxHealth;
        this.animator = this.GetComponent<Animator>();
    }

    private void Update() {
        if (this.cooldownTimer > 0)
            this.cooldownTimer -= Time.deltaTime;
    }

    public void TakeDamage(float amount) {
        if (this.cooldownTimer > 0)
            return;
        this.cooldownTimer = this.cooldown;

        this.currHealth -= amount;
        if (this.currHealth <= 0) {
            Destroy(this.gameObject);
        }

        this.animator.SetTrigger(Hurt);
    }

}