using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public Transform following;
    public float yOffset;

    private Vector2 currVelocity;

    private void Update() {
        var trans = this.transform;
        var goal = this.following.position;
        trans.position = new Vector3(goal.x, goal.y + this.yOffset, trans.position.z);
    }

}