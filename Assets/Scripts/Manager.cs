using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum State{
	MENU,
	PLAY,
	CREDITS,
	QUIT,
	LEVEL1,
	LEVEL2,
	LEVEL3,
}

public  class Manager : MonoBehaviour {


	// Singleton
	static public Manager instance;
	public State state;

    public AudioManager audioManager;

	// Variable btn
	public GameObject guiCredits;
	public GameObject playBtn;
	public GameObject quitBtn;
	public GameObject creditsBtn;

    OVRScreenFade screenFade;

	// Index to Load
	private int levelToLoad = 2;


	void Awake(){

        screenFade = GameObject.Find("LeftEye").GetComponent<OVRScreenFade>();
        screenFade = GameObject.Find("RightEye").GetComponent<OVRScreenFade>();

        if (instance !=null) Debug.LogError("Double singleton");
			instance = this;
		DontDestroyOnLoad (this);

		guiCredits.SetActive (false);

	}

	// Method to quit game
	public void OnClickQuit(){
		Application.Quit ();
		Debug.Log ("Quit");
	}

	// Method to play game
	public void OnClickPlay(){
		SceneManager.LoadSceneAsync (levelToLoad);
        screenFade.StartCoroutine("FadeOut");
        
	}

	// Method to active Credits
	public void OnClickCredit(){
		
		guiCredits.SetActive (true);
		playBtn.SetActive (false);
		quitBtn.SetActive (false);
		creditsBtn.SetActive (false);
		
	}

	// Method to back to the Menu
	public void OnClickBack(){
		
		guiCredits.SetActive (false);
		playBtn.SetActive (true);
		quitBtn.SetActive (true);
		creditsBtn.SetActive (true);

	}
}
