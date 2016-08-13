using UnityEngine;
using System.Collections;

public class EnergyElement : MonoBehaviour {
	public EnergyApplication app { 
		get { 
			return GameObject.FindObjectOfType<EnergyApplication>(); 
		}
	}
}


public class EnergyApplication : MonoBehaviour {

	public EnergyModel model;
	public EnergyController controller;
	public EnergyView view;

	void Start(){
		
	}
}
