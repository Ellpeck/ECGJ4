﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    private static readonly int Hurt = Animator.StringToHash("Hurt");

    public float maxHealth;
    public float currHealth;
    public float cooldown;
    public Spirit spirit;
    public bool destroyOnDeath = true;
    public UnityEvent onDeath;

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

    public bool TakeDamage(float amount) {
        if (this.cooldownTimer > 0)
            return false;
        this.cooldownTimer = this.cooldown;

        this.currHealth -= amount;
        if (this.currHealth <= 0) {
            if (this.destroyOnDeath)
                Destroy(this.gameObject);
            this.onDeath.Invoke();
            return true;
        }

        this.animator.SetTrigger(Hurt);
        return false;
    }

}