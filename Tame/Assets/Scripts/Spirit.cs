﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour {

    public string identifier;
    public bool canHaveMultiple;
    public float lastingTime;
    public float addedSpeed;
    public float addedJumpForce;
    public float addedJumpTime;
    public int addedHeaviness;
    public GameObject deathEffect;

    private float timer;
    private bool isApplied;

    private Player player;
    private DistanceJoint2D joint;

    private void Start() {
        this.player = FindObjectOfType<Player>();
        this.joint = this.GetComponent<DistanceJoint2D>();
        this.joint.connectedBody = this.player.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (this.isApplied)
            this.timer += Time.deltaTime;
    }

    public bool IsDone() {
        return this.isApplied && this.timer >= this.lastingTime;
    }

    public void Destroy() {
        Instantiate(this.deathEffect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void Apply(Player player) {
        if (!this.isApplied) {
            player.speed += this.addedSpeed;
            player.jumpForce += this.addedJumpForce;
            player.jumpTime += this.addedJumpTime;
            player.heaviness += this.addedHeaviness;
            this.isApplied = true;
        }
    }

    public void Unapply(Player player) {
        if (this.isApplied) {
            player.speed -= this.addedSpeed;
            player.jumpForce -= this.addedJumpForce;
            player.jumpTime -= this.addedJumpTime;
            player.heaviness -= this.addedHeaviness;
            this.isApplied = false;
        }
    }

}