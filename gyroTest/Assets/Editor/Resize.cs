using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Resize : ScriptableObject
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [MenuItem("Yu/Resize")]
    public static void ResizeSelection()
    {
        foreach (var go in Selection.gameObjects)
        {
            go.transform.position = new Vector3(go.transform.position.x / 10, go.transform.position.y / 10, go.transform.position.z / 10);
            go.transform.localScale = new Vector3(1, go.transform.localScale.y / 10, 1);
        }
    }
}
