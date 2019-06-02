using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour {

    public int indexInLevel;

    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if (player) {
            player.hasPuzzlePieces[this.indexInLevel] = true;
            Destroy(this.gameObject);
        }
    }

}