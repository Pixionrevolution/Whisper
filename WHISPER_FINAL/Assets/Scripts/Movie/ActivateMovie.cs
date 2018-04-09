using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ActivateMovie : MonoBehaviour
{
    // Variables to reference the scene
    private LevelManager levelManager;

    // Use this for initialization
    void Start()
    {
        // Variables to get Videoplayer Component
        var videoPlayer = GetComponent<VideoPlayer>();

        //Active checkfonction
        videoPlayer.loopPointReached += CheckOver;
    }

    // Fonction for checking if the video is over.
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        GameObject.Find("[MANAGER]").GetComponent<LevelManager>().next();
    }
}
