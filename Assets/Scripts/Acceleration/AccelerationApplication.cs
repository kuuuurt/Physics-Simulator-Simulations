using UnityEngine;
using System.Collections;

public class AccelerationElement : MonoBehaviour {
	public AccelerationApplication app { 
		get { 
			return GameObject.FindObjectOfType<AccelerationApplication>(); 
		}
	}
}


public class AccelerationApplication : MonoBehaviour {

	public AccelerationModel model;
	public AccelerationController controller;
	public AccelerationView view;

	void Start(){
		
	}
}
