using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazePointLeft : MonoBehaviour {

	FoveInterface  FI;

	// Use this for initialization
	void Start () {
		FI = FindObjectOfType<FoveInterface>();
	}
	
	// Update is called once per frame
	void Update () {

		//FoveInterface.EyeRays rays = FoveInterface.GetEyeRays();
		FoveInterface.EyeRays rays  = FI.GetGazeRays();
		// TODO: calculate the convergence point in FoveInterface

		// Just hack in to use the right eye for now...
		RaycastHit hit;
		Physics.Raycast(rays.left, out hit, Mathf.Infinity);
		if (hit.point != Vector3.zero) // Vector3 is non-nullable; comparing to null is always false
		{
			transform.position = hit.point + new Vector3(0,0,2);
        }
        else
		{
			transform.position = rays.left.GetPoint(10.0f);
		}
	}
}
