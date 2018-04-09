using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fove.Managed;


public class AnimateButton : MonoBehaviour {

    //Canvas loading Bar
	public Transform loadingRectangle;
    // AudioClip to Add
	public AudioClip soundEndLoading;
    public AudioClip soundLoading;

    //Variable main sound
    public AudioSource soundMain;
    private float soundMainVolume = 0.5f;

    // Requiered components
	private Button button{ get { return GetComponent<Button> (); } }
    public AudioSource sourceEnd;
    public AudioSource sourceLoading;

    // Variables for fillAmount loadingBar
    [Range(0.0f,1.0f)]
	[SerializeField]private float currentAmount;
	[SerializeField]private float speed;

    //Variables for activate loadingBar on Collision with GazeEyesPoints
    public bool activeLoadingBar = false;

    //Variable acces manager for launch button
    public Manager manager;

    void Start()
    {
        soundMainVolume += 0.5f * Time.deltaTime;
    }

	// Update is called once per frame
	void Update ()
    {
        // Condition to detect if the collider
        if( GetComponent<InteractUI>().touchedCollider == true)
        {    //Condition to "fillAmount" of the loading bar
            if (currentAmount <= 100)
            {   
                
                FadeOutMainSound();
                currentAmount += speed * Time.deltaTime;
                PlaySoundLoading();

                //Condition to Activate the Interaction
                if (currentAmount >= 100 )
                {   
                    //Play Button
                    if (GetComponent<InteractUI>().colliderButton.name == "ColliderPlay")
                    {
                        manager.OnClickPlay();
                        PlaySoundEnd();
                    }
                    //Credits Button
                    if (GetComponent<InteractUI>().colliderButton.name == "ColliderCredits")
                    {
                        manager.OnClickCredit();
                        PlaySoundEnd();
                    } else
                    {
                         //Quit Button
                        manager.OnClickQuit();
                        PlaySoundEnd();
                    }
                }
            }  // Condition to "smooth mute" 
        } else if (GetComponent<InteractUI>().touchedCollider == false){
            PlaySoundLoadingMute();
        }
        // Condition to decrease the loading bar when the target look out
       if(GetComponent<InteractUI>().touchedCollider == false && currentAmount > 0)
        {
            currentAmount -= speed * Time.deltaTime;
            FadeInMainSound();
        }
       
		loadingRectangle.GetComponent<Image> ().fillAmount = currentAmount / 100;
	}

    // Method play One Shot the sound of button end
	void PlaySoundEnd()
    {
        sourceEnd.PlayOneShot (soundEndLoading);    
	}

    // Method play One Shot the sound of button end
    void PlaySoundLoading()
    {
        sourceLoading.mute = false;
        sourceLoading.PlayOneShot(soundLoading);
    }

    //Mute the sound of the feel
    void PlaySoundLoadingMute()
    {
        sourceLoading.mute = true;
        sourceLoading.time = 0;
    }

    //Methode to fade in the main sound
    void FadeInMainSound()
    {
        if (soundMainVolume < 1)
        {
            soundMainVolume += 0.1f * Time.deltaTime;
            soundMain.volume = soundMainVolume;
        }
    }

    //Methode to fade Out the main sound
    void FadeOutMainSound()
    {
        if (soundMainVolume > 0.1)
        {
            soundMainVolume -= 0.1f * Time.deltaTime;
            soundMain.volume = soundMainVolume;
        }
    }
}
