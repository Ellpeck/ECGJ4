using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour {

    public int indexInLevel;
    public GameObject collectEffect;
    public AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if (player) {
            player.hasPuzzlePieces[this.indexInLevel] = true;

            var position = this.transform.position;
            Instantiate(this.collectEffect, position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(this.collectSound, position);
            Destroy(this.gameObject);
        }
    }

}