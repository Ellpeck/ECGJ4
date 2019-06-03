using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour {

    public float speed;
    public Transform borderCheck;
    public Transform groundCheck;
    public LayerMask groundLayers;

    private Rigidbody2D body;

    private bool facingRight;

    private void Start() {
        this.body = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        var filter = new ContactFilter2D();
        filter.SetLayerMask(this.groundLayers);
        bool onGround = Physics2D.defaultPhysicsScene.OverlapCircle(this.groundCheck.position, 0.1F, filter);
        if (onGround) {
            var groundInFront = Physics2D.defaultPhysicsScene.OverlapCircle(this.borderCheck.position, 0.5F, filter);
            if (!groundInFront) {
                this.facingRight = !this.facingRight;
                this.transform.Rotate(0, 180, 0);
            }
            this.body.velocity = new Vector2(this.facingRight ? this.speed : -this.speed, 0);
        }
    }

}