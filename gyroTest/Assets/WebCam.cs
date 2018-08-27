using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCam : MonoBehaviour {

	public WebCamTexture cameraTexture;
	public string cameraName = "";
	public RawImage rawimage;

	// Use this for initialization
	void Start () {
		StartCoroutine(Test());
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator Test()
	{
		yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
		if (Application.HasUserAuthorization(UserAuthorization.WebCam))
		{
			WebCamDevice[] devices = WebCamTexture.devices;
			cameraName = devices[0].name;
			cameraTexture = new WebCamTexture(cameraName,1920,1080);

			rawimage.texture = cameraTexture;
			cameraTexture.Play();

		}
	}
}
