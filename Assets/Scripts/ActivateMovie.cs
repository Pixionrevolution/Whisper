using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ActivateMovie : MonoBehaviour
{
	// Variables to reference the scene
	public string nextScene;
    
    // Use this for initialization
    void Start () {

        // Find the screen
        GameObject screen = GameObject.Find ("Screen");

        // Variables to get Videoplayer Component
        var videoPlayer = screen.GetComponent<VideoPlayer> ();
       
		//Active checkfonction
		videoPlayer.loopPointReached += CheckOver;
     
	}

	// Fonction for checking if the video is over.
	void CheckOver(UnityEngine.Video.VideoPlayer vp)
	{
		//print ("Video is Over");
		// Load scene
		SceneManager.LoadScene (nextScene);

    }

}
