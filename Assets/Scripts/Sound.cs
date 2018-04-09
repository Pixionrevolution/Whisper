using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound  {

    // Name of the track
    public string name;

    // Clip of the track
    public AudioClip clip;
    public int currentClip=0;

    // Range volume of the track
    [Range(0.1f, 1.0f)]
    public float volume;

    // Range pitch of the track
    [Range(0.1f, 1.0f)]
    public float pitch;

    // Boolean to Loop the track
    public bool loop;
    // Boolean to spatialize the track
    public bool spatialize;
    // Boolean to play on awake the track
    public bool playOnAwake;

    //public bool mute;

    // AudioSource
    [HideInInspector]
    public AudioSource source;


}
