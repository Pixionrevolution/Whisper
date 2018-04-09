using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public int currentScene = 0;
	public Save save;

	public int firstLevelId = 3;

	public string[] scenes;

	public OVRScreenFade fadeL;
	public OVRScreenFade fadeR;

	public void Start() {
        // Load first scene
        SceneManager.LoadSceneAsync (scenes[currentScene], LoadSceneMode.Additive);

        save = GetComponent<Avancement>().save;
    }

	public void next() {
		// If there is a next scene to load ...
		if (currentScene + 1 < scenes.Length) {
            StartCoroutine("doNext", currentScene + 1);
		} else {
			Debug.Log("Fin du game");
            goToScene(1);
			save.level = -1;
		}
	}

    public void goTo(int level)
    {
        StartCoroutine("doNext", level + firstLevelId - 1);
    }

    public void goToScene(int scene)
    {
        StartCoroutine("doNext", scene);
    }

    IEnumerator doNext(int _next)
    {
        fadeL.StartCoroutine("FadeLevel");
        fadeR.StartCoroutine("FadeLevel");

        yield return new WaitForSeconds(4.5f);

        // Replace Scene
        SceneManager.UnloadSceneAsync(scenes[currentScene]);
        currentScene = _next;
        SceneManager.LoadScene(scenes[currentScene], LoadSceneMode.Additive);


        // Save Level
        int level = currentScene - firstLevelId;

        if (level >= 0)
        {
            save.level = level + 1; // Counting from 1
        }
    }
}
