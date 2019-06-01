using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Bouncer : MonoBehaviour {

    private static readonly int Jumping = Animator.StringToHash("Jumping");

    public float speed;
    public float jumpForce;
    public float jumpWait;
    public Transform borderCheck;
    public Transform groundCheck;
    public LayerMask groundLayers;

    private Rigidbody2D body;
    private Animator animator;

    private bool facingRight;
    private float jumpWaitTimer;

    private void Start() {
        this.body = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
    }

    private void FixedUpdate() {
        var onGround = Physics2D.OverlapCircle(this.groundCheck.position, 0.1F, this.groundLayers);
        if (onGround) {
            var groundInFront = Physics2D.OverlapCircle(this.borderCheck.position, 0.5F, this.groundLayers);
            if (!groundInFront) {
                this.facingRight = !this.facingRight;
                this.transform.Rotate(0, 180, 0);
            }

            this.jumpWaitTimer += Time.deltaTime;
            if (this.jumpWaitTimer >= this.jumpWait) {
                this.body.velocity = new Vector2(this.facingRight ? this.speed : -this.speed, this.jumpForce);
                this.animator.SetBool(Jumping, true);
                this.jumpWaitTimer = 0;
            } else {
                this.animator.SetBool(Jumping, false);
            }
        }
    }

}