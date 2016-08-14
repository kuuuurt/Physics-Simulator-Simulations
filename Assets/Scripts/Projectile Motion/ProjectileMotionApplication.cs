using UnityEngine;
using System.Collections;

public class ProjectileMotionElement : MonoBehaviour {
	public ProjectileMotionApplication app { 
		get { 
			return GameObject.FindObjectOfType<ProjectileMotionApplication>(); 
		}
	}
}


public class ProjectileMotionApplication : MonoBehaviour {

	public ProjectileMotionModel model;
	public ProjectileMotionController controller;
	public ProjectileMotionView view;

	void Start(){
		
	}
}
