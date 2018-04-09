using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using Fove.Managed;

public class DepthOfFieldControll : MonoBehaviour {

    public PostProcessingProfile postProfile;

	// Use this for initialization
	void Start () {



		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            postProfile.depthOfField.enabled = true;


        }
		
	}



}
