using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public string firstLevel;

    public void OnPlayButton() {
        this.StartCoroutine(this.StartGame());
    }

    private IEnumerator StartGame() {
        Fade.Instance.FadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(this.firstLevel);
    }

}