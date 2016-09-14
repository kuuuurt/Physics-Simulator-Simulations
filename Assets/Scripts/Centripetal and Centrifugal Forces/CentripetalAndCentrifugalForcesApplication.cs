using UnityEngine;
using System.Collections;

public class CentripetalAndCentrifugalForcesElement : MonoBehaviour {
	public CentripetalAndCentrifugalForcesApplication app { 
		get { 
			return GameObject.FindObjectOfType<CentripetalAndCentrifugalForcesApplication>(); 
		}
	}
}


public class CentripetalAndCentrifugalForcesApplication : MonoBehaviour {

	public CentripetalAndCentrifugalForcesModel model;
	public CentripetalAndCentrifugalForcesController controller;
	public CentripetalAndCentrifugalForcesView view;

	void Start(){
		
	}
}
