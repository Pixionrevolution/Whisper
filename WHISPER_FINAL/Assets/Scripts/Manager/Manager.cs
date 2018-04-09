using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
    // Variable btn
    public GameObject guiCredits;
    public GameObject playBtn;
    public GameObject quitBtn;
    public GameObject creditsBtn;

    private LevelManager levelManager;
    private int savedLevel;
    private GameObject manager;

    void Awake()
    {
        manager = GameObject.Find("[MANAGER]");

        savedLevel = manager.GetComponent<Avancement>().save.level;
        levelManager = manager.GetComponent<LevelManager>();

        Debug.Log(savedLevel);
    }

    // Method to quit game
    public void OnClickQuit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    // Method to play game
    public void OnClickPlay()
    {
        if (savedLevel >= 0)
            levelManager.goTo(savedLevel);
        else
            levelManager.next();
    }

    // Method to active Credits
    public void OnClickCredit()
    {
        guiCredits.SetActive(true);
        playBtn.SetActive(false);
        quitBtn.SetActive(false);
        creditsBtn.SetActive(false);

    }

    // Method to back to the Menu
    public void OnClickBack()
    {
        guiCredits.SetActive(false);
        playBtn.SetActive(true);
        quitBtn.SetActive(true);
        creditsBtn.SetActive(true);
    }
}
