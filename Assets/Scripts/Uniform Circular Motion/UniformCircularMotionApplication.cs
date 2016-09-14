using UnityEngine;
using System.Collections;

public class UniformCircularMotionElement : MonoBehaviour {
	public UniformCircularMotionApplication app { 
		get { 
			return GameObject.FindObjectOfType<UniformCircularMotionApplication>(); 
		}
	}
}


public class UniformCircularMotionApplication : MonoBehaviour {

	public UniformCircularMotionModel model;
	public UniformCircularMotionController controller;
	public UniformCircularMotionView view;

	void Start(){
		
	}
}
