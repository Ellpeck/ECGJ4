using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {

    private new LineRenderer renderer;
    private Player player;

    private void Start() {
        this.renderer = this.GetComponent<LineRenderer>();
        this.player = FindObjectOfType<Player>();
    }

    private void Update() {
        this.renderer.SetPosition(0, this.transform.position);
        this.renderer.SetPosition(1, this.player.transform.position);
    }

}