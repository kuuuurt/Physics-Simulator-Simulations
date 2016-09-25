using UnityEngine;
using System.Collections;

public class WorkPowerElement : MonoBehaviour {
	public WorkPowerApplication app { 
		get { 
			return GameObject.FindObjectOfType<WorkPowerApplication>(); 
		}
	}
}


public class WorkPowerApplication : MonoBehaviour {

	public WorkPowerModel model;
	public WorkPowerController controller;
	public WorkPowerView view;

	void Start(){
		
	}
}
