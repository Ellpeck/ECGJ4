using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Jumping = Animator.StringToHash("Jumping");

    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask groundLayers;

    private Animator animator;
    private Rigidbody2D body;

    private float moveInput;
    private bool jump;
    private bool facingRight;

    private void Start() {
        this.animator = this.GetComponent<Animator>();
        this.body = this.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        this.moveInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
            this.jump = true;
    }

    private void FixedUpdate() {
        this.body.velocity = new Vector2(this.moveInput * this.speed, this.body.velocity.y);
        if (this.moveInput != 0 && this.moveInput > 0 != this.facingRight) {
            this.facingRight = this.moveInput > 0;
            this.transform.Rotate(0, 180, 0);
        }
        this.animator.SetBool(Walking, this.moveInput != 0);

        bool onGround = Physics2D.OverlapCircle(this.groundCheck.position, 0.1F, this.groundLayers);
        if (onGround) {
            if (this.jump) {
                this.body.velocity = new Vector2(this.body.velocity.x, this.jumpForce);
                this.animator.SetBool(Jumping, true);
            } else {
                this.animator.SetBool(Jumping, false);
            }
        }
        this.jump = false;
    }

}