using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIButton : MonoBehaviour {

    
    private float distanceEyeObjet;
    public GameObject foveCam;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.LookAt(foveCam.transform.position, -Vector3.up);

      

    }
}
