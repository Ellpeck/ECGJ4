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
        var filter = new ContactFilter2D();
        filter.SetLayerMask(this.groundLayers);
        bool onGround = Physics2D.defaultPhysicsScene.OverlapCircle(this.groundCheck.position, 0.1F, filter);
        if (onGround) {
            this.jumpWaitTimer += Time.deltaTime;
            if (this.jumpWaitTimer >= this.jumpWait) {
                var groundInFront = Physics2D.defaultPhysicsScene.OverlapCircle(this.borderCheck.position, 0.5F, filter);
                if (!groundInFront) {
                    this.facingRight = !this.facingRight;
                    this.transform.Rotate(0, 180, 0);
                }
                
                this.body.velocity = new Vector2(this.facingRight ? this.speed : -this.speed, this.jumpForce);
                this.animator.SetBool(Jumping, true);
                this.jumpWaitTimer = 0;
            } else {
                this.animator.SetBool(Jumping, false);
            }
        }
    }

}