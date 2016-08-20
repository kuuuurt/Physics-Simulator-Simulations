using UnityEngine;
using System.Collections;

public class PlatformRotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, new Vector3(0,0,45), Time.deltaTime * 0.1f);
	}
}
