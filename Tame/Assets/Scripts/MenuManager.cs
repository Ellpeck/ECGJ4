using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void LoadLevel(string level) {
        this.StartCoroutine(LoadScene(level));
    }

    private static IEnumerator LoadScene(string name) {
        Fade.Instance.FadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(name);
    }

}