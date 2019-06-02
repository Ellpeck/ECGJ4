using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private static readonly int AnimSpawn = Animator.StringToHash("Spawn");

    public GameObject monsterToSpawn;
    public float spawnInterval;
    public Transform spawnPoint;

    private Animator animator;

    private bool playerInRange;
    private float spawnTimer;

    private void Start() {
        this.animator = this.GetComponent<Animator>();
    }

    private void Update() {
        if (this.playerInRange) {
            if (this.spawnTimer >= this.spawnInterval) {
                this.spawnTimer = 0;
                this.animator.SetTrigger(AnimSpawn);
            } else {
                this.spawnTimer += Time.deltaTime;
            }
        }
    }

    [UsedImplicitly]
    public void Spawn() {
        Instantiate(this.monsterToSpawn, this.spawnPoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            this.playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            this.playerInRange = false;
        }
    }

}