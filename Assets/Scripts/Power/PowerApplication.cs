using UnityEngine;
using System.Collections;

public class PowerElement : MonoBehaviour {
	public PowerApplication app { 
		get { 
			return GameObject.FindObjectOfType<PowerApplication>(); 
		}
	}
}


public class PowerApplication : MonoBehaviour {

	public PowerModel model;
	public PowerController controller;
	public PowerView view;

	void Start(){
		
	}
}
