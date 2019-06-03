using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {

    public SpriteRenderer[] pieces;
    public Color completedColor;
    public string nextLevel;
    private bool isCompleted;
    public GameObject fillEffect;

    private void OnTriggerEnter2D(Collider2D other) {
        if (this.isCompleted)
            return;

        var player = other.GetComponent<Player>();
        if (player && player.HasAllPieces()) {
            player.isMovementBlocked = true;
            this.StartCoroutine(this.FillOutPieces());
            this.isCompleted = true;
        }
    }

    private IEnumerator FillOutPieces() {
        foreach (var piece in this.pieces) {
            piece.color = this.completedColor;
            Instantiate(this.fillEffect, piece.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);
        Fade.Instance.FadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(this.nextLevel);
    }

}