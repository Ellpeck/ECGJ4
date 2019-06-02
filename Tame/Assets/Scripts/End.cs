using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {

    public SpriteRenderer[] pieces;
    public Color completedColor;
    public string nextLevel;
    private bool isCompleted;

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
            yield return new WaitForSeconds(0.5F);
        }

        yield return new WaitForSeconds(1);
        Fade.Instance.FadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(this.nextLevel);
    }

}