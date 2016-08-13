using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoaderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frames
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			goBack ();
		}
	}

	public void LoadScene(string scene){
		SceneManager.LoadScene (scene);
	}

	public void goBack(){
		using (AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
			using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject> ("currentActivity")) {
				jo.Call ("onBackPressed");
			}
		}
	}
		
}
