using UnityEngine;
using System.Collections;

public class VelocityElement : MonoBehaviour {
	public VelocityApplication app { 
		get { 
			return GameObject.FindObjectOfType<VelocityApplication>(); 
		}
	}
}


public class VelocityApplication : MonoBehaviour {

	public VelocityModel model;
	public VelocityController controller;
	public VelocityView view;

	void Start(){
		
	}
}
