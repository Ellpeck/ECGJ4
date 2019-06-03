using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {

    public SpriteRenderer[] pieces;
    public Color completedColor;
    public string nextLevel;
    private bool isCompleted;
    public GameObject fillEffect;
    public AudioClip placeSound;

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
            var position = piece.transform.position;
            Instantiate(this.fillEffect, position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(this.placeSound, position);
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);
        Fade.Instance.FadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(this.nextLevel);
    }

}