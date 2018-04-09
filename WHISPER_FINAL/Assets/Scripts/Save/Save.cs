using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int level = 0;

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }

    public static Save CreateFromJSON(string json)
    {
        return JsonUtility.FromJson<Save>(json);
    }
}