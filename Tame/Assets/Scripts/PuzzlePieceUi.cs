using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceUi : MonoBehaviour {

    public Color gottenColor;
    public Color notGottenColor;
    public int index;

    private Player player;
    private new Image renderer;

    private bool gotten = true;

    private void Start() {
        this.player = FindObjectOfType<Player>();
        this.renderer = this.GetComponent<Image>();
    }

    private void Update() {
        var has = this.player.hasPuzzlePieces[this.index];
        if (has != this.gotten) {
            this.gotten = has;
            this.renderer.color = this.gotten ? this.gottenColor : this.notGottenColor;
        }
    }

}