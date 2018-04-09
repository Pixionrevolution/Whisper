using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public GameObject[] props;


     void Awake()
    {
        foreach (Sound s in sounds)
        {
           foreach(GameObject p in props)
            {
                  if (s.name == p.name)
                    {
                        s.source = p.AddComponent<AudioSource>();
                        s.source.clip = s.clip;

                        s.source.volume = s.volume;
                        s.source.pitch = s.pitch;
                        s.source.spatialize = s.spatialize;
                        s.source.playOnAwake = s.playOnAwake;
                    }
            }
        }
    }

   /* void Start()
    {
        Play("Theme");
    }*/

    /* public void Play(string name)
    {

       Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }*/

  
}
