using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask groundLayers;

    private Rigidbody2D body;

    private float moveInput;
    private bool jump;

    private void Start() {
        this.body = this.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        this.moveInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
            this.jump = true;
    }

    private void FixedUpdate() {
        this.body.velocity = new Vector2(this.moveInput * this.speed, this.body.velocity.y);

        if (this.jump) {
            bool onGround = Physics2D.OverlapCircle(this.groundCheck.position, 0.1F, this.groundLayers);
            if (onGround) {
                this.body.velocity = new Vector2(this.body.velocity.x, this.jumpForce);
            }
            this.jump = false;
        }
    }

}