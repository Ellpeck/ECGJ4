using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUi : MonoBehaviour {

    public Color fullColor;
    public Color emptyColor;
    public int index;

    private Health playerHealth;
    private new Image renderer;

    private bool isFull = true;

    private void Start() {
        this.playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        this.renderer = this.GetComponent<Image>();
    }

    private void Update() {
        var has = this.playerHealth.currHealth > this.index;
        if (has != this.isFull) {
            this.isFull = has;
            this.renderer.color = this.isFull ? this.fullColor : this.emptyColor;
        }
    }

}