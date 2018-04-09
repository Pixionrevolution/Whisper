using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugNextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("nextLevel");
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator nextLevel() {
		 yield return new WaitForSeconds(6);
        // Change level
		 GameObject.Find("[MANAGER]").GetComponent<LevelManager>().next();
	}
}
