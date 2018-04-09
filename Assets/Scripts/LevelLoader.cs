using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
	
	// GameObject Loading Screen
	public GameObject loadingScreen;
	// GameObject Slider 
	public Slider slider;
	// Text pourcentage
	public Text progressText;

	// Loading level fonction with sceneIndex type ID
	public void LoadLevel(int sceneIndex)
	{
		// Screen Loading Activation
		loadingScreen.SetActive (true);
		// Start Coroutine
		StartCoroutine (LoadAsynchronously (sceneIndex));
	}

	//Coroutine of loading level
	IEnumerator LoadAsynchronously (int sceneIndex)
	{

		AsyncOperation operation =  SceneManager.LoadSceneAsync (sceneIndex);

		while (!operation.isDone) 
			
		{

			// Screen Loading Activation
			//loadingScreen.SetActive (true);

			// Clamping progress bar from 0 to 1
			float progress = Mathf.Clamp01 (operation.progress / .9f);
			// Moving slider
			slider.value = progress;
			//Incrementation of text loading %
			progressText.text = progress * 100.0f + "%";

			//Debug.Log (operation.progress);

			yield return null;
		}

	}
}
