using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftCorrection : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        this.GetComponent<Renderer>().enabled = true;
    }

    // Update is called once per frame
    void Update () {

        this.GetComponent<Renderer>().enabled = true;
            
		if (this.GetComponent<Renderer>().enabled)
		{
			RaycastHit hit;
			FoveInterface foveInterface = transform.GetComponent<FoveInterface>();
			Ray ray = new Ray(foveInterface.transform.position, foveInterface.transform.forward);
			if (Physics.Raycast(ray, out hit, 10.0f))
			{
				transform.position = hit.point;
			}
			else
			{
				transform.position = foveInterface.transform.position + foveInterface.transform.forward * 1.5f;
			}
		}

		/*if (Input.GetKeyUp(KeyCode.M))
		{
			this.GetComponent<Renderer>().enabled = false;
			Fove.FoveHeadset.GetHeadset().ManualDriftCorrection3D(transform.localPosition);
		}*/
		
	}
}
