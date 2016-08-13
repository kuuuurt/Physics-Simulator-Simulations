using UnityEngine;
using System.Collections;

public class FreefallElement : MonoBehaviour {
	public FreefallApplication app { 
		get { 
			return GameObject.FindObjectOfType<FreefallApplication>(); 
		}
	}
}


public class FreefallApplication : MonoBehaviour {

	public FreefallModel model;
	public FreefallController controller;
	public FreefallView view;

	void Start(){
		
	}
}
