using UnityEngine;
using System.Collections;

public class RotationalMotionElement : MonoBehaviour {
	public RotationalMotionApplication app { 
		get { 
			return GameObject.FindObjectOfType<RotationalMotionApplication>(); 
		}
	}
}


public class RotationalMotionApplication : MonoBehaviour {

	public RotationalMotionModel model;
	public RotationalMotionController controller;
	public RotationalMotionView view;

	void Start(){
		
	}
}
