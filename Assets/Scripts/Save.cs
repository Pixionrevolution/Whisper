using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class Save  {

	public int level = 0;

	public string ToJSON(){
		return JsonUtility.ToJson (this);
	}

	public static Save CreateFromJson(string json){
		
		return JsonUtility.FromJson<Save> (json);
	}

}
