using UnityEngine;
using System.Collections;

public class FrictionElement : MonoBehaviour {
	public FrictionApplication app { 
		get { 
			return GameObject.FindObjectOfType<FrictionApplication>(); 
		}
	}
}


public class FrictionApplication : MonoBehaviour {

	public FrictionModel model;
	public FrictionController controller;
	public FrictionView view;

	void Start(){
		
	}
}
