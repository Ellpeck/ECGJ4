﻿using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Shoot = Animator.StringToHash("Shoot");

    public float speed;
    public float jumpForce;
    public float jumpTime;
    public Transform groundCheck;
    public LayerMask groundLayers;
    public Transform projectileOrigin;
    public GameObject normalProjectile;
    public GameObject conversionProjectile;
    public float shootCooldown;
    public List<Spirit> appliedUpgrades;
    public bool[] hasPuzzlePieces = new bool[4];
    public bool isMovementBlocked;
    public int heaviness;
    public AudioClip shootSound;

    private Animator animator;
    private Rigidbody2D body;

    private float moveInput;
    private bool startJump;
    private bool facingRight;
    private bool useConversion;
    private float currShootCooldown;
    private float jumpTimer;
    private bool isInJump;

    private void Start() {
        this.animator = this.GetComponent<Animator>();
        this.body = this.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!this.isMovementBlocked) {
            this.moveInput = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump"))
                this.startJump = true;

            if (this.currShootCooldown <= 0) {
                var conversion = Input.GetButton("Fire2");
                if (conversion || Input.GetButton("Fire1")) {
                    this.animator.SetTrigger(Shoot);
                    this.useConversion = conversion;
                    this.currShootCooldown = this.shootCooldown;
                }
            } else {
                this.currShootCooldown -= Time.deltaTime;
            }
        } else {
            this.moveInput = 0;
        }

        for (var i = this.appliedUpgrades.Count - 1; i >= 0; i--) {
            var upgrade = this.appliedUpgrades[i];
            if (upgrade.IsDone()) {
                upgrade.Unapply(this);
                upgrade.Destroy();
                this.appliedUpgrades.RemoveAt(i);
            }
        }
    }

    private void FixedUpdate() {
        this.body.velocity = new Vector2(this.moveInput * this.speed, this.body.velocity.y);
        if (this.moveInput != 0 && this.moveInput > 0 != this.facingRight) {
            this.facingRight = this.moveInput > 0;
            this.transform.Rotate(0, 180, 0);
        }
        this.animator.SetBool(Walking, this.moveInput != 0);

        var filter = new ContactFilter2D();
        filter.SetLayerMask(this.groundLayers);
        bool onGround = Physics2D.defaultPhysicsScene.OverlapCircle(this.groundCheck.position, 0.1F, filter);
        if (onGround) {
            if (this.startJump) {
                this.body.velocity = new Vector2(this.body.velocity.x, this.jumpForce);
                this.isInJump = true;
                this.jumpTimer = this.jumpTime;
                this.animator.SetBool(Jumping, true);
            } else {
                this.animator.SetBool(Jumping, false);
            }
        }
        this.startJump = false;

        if (Input.GetButton("Jump")) {
            if (this.isInJump) {
                if (this.jumpTimer > 0) {
                    this.jumpTimer -= Time.deltaTime;
                    this.body.velocity = new Vector2(this.body.velocity.x, this.jumpForce);
                } else {
                    this.isInJump = false;
                }
            }
        } else {
            this.isInJump = false;
        }
    }

    public bool HasAllPieces() {
        foreach (var piece in this.hasPuzzlePieces)
            if (!piece)
                return false;
        return true;
    }

    [UsedImplicitly]
    public void ShootProjectile() {
        var prefabToUse = this.useConversion ? this.conversionProjectile : this.normalProjectile;
        Instantiate(prefabToUse, this.projectileOrigin.position, this.projectileOrigin.rotation);
        AudioSource.PlayClipAtPoint(this.shootSound, this.transform.position);
    }

    public void ApplyUpgrade(Spirit upgrade, Vector3 position) {
        if (!upgrade.canHaveMultiple) {
            foreach (var curr in this.appliedUpgrades) {
                if (curr.identifier == upgrade.identifier)
                    return;
            }
        }

        var inst = Instantiate(upgrade, position, Quaternion.identity);
        inst.Apply(this);
        this.appliedUpgrades.Add(inst);
    }

    public void OnDeath() {
        this.isMovementBlocked = true;
        this.StartCoroutine(this.ReloadScene());
    }

    private IEnumerator ReloadScene() {
        Fade.Instance.FadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}