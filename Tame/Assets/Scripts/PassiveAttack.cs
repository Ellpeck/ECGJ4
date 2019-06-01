using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAttack : MonoBehaviour {

    public float damage;
    public Collider2D attackCollider;
    public LayerMask attackLayers;

    private readonly List<Collider2D> collidersTemp = new List<Collider2D>();

    private void Update() {
        this.collidersTemp.Clear();

        var filter = new ContactFilter2D();
        filter.SetLayerMask(this.attackLayers);
        Physics2D.OverlapCollider(this.attackCollider, filter, this.collidersTemp);
        foreach (var coll in this.collidersTemp) {
            var health = coll.GetComponent<Health>();
            if (health)
                health.TakeDamage(this.damage);
        }
    }

}