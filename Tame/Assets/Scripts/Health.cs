using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float currHealth;

    private void Start() {
        this.currHealth = this.maxHealth;
    }

    public void TakeDamage(float amount) {
        this.currHealth -= amount;
        if (this.currHealth <= 0) {
            Destroy(this.gameObject);
        }
    }

}