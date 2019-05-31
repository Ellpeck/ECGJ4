using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public bool isConversion;
    public float damage;
    public float speed;
    public float deathTime;

    private Rigidbody2D body;
    private float aliveTimer;

    private void Start() {
        this.body = this.GetComponent<Rigidbody2D>();
        this.body.velocity = this.transform.right * this.speed;
    }

    private void Update() {
        this.aliveTimer += Time.deltaTime;
        if (this.aliveTimer >= this.deathTime) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var health = other.gameObject.GetComponent<Health>();
        if (health != null)
            health.TakeDamage(this.damage);

        Destroy(this.gameObject);
    }

}