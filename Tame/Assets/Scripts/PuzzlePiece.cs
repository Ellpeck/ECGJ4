using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour {

    public int indexInLevel;
    public GameObject collectEffect;

    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if (player) {
            player.hasPuzzlePieces[this.indexInLevel] = true;

            Instantiate(this.collectEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}