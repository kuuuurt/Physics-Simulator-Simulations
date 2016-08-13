using UnityEngine;
using System.Collections;

public class WorkElement : MonoBehaviour {
	public WorkApplication app { 
		get { 
			return GameObject.FindObjectOfType<WorkApplication>(); 
		}
	}
}


public class WorkApplication : MonoBehaviour {

	public WorkModel model;
	public WorkController controller;
	public WorkView view;

	void Start(){
		
	}
}
