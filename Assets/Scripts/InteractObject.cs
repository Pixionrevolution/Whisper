using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour {

    public string nameSound;

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "GazePoints")
        {
            //FindObjectOfType<AudioManager>().Play("Test");
        }
    }
}
