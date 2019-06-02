using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public float yOffset;

    private Vector2 currVelocity;
    private Transform following;

    private void Start() {
        this.following = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        var trans = this.transform;
        var goal = this.following.position;
        trans.position = new Vector3(goal.x, goal.y + this.yOffset, trans.position.z);
    }

}