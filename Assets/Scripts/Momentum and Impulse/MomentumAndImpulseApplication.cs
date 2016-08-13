using UnityEngine;
using System.Collections;

public class MomentumAndImpulseElement : MonoBehaviour {
	public MomentumAndImpulseApplication app { 
		get { 
			return GameObject.FindObjectOfType<MomentumAndImpulseApplication>(); 
		}
	}
}


public class MomentumAndImpulseApplication : MonoBehaviour {

	public MomentumAndImpulseModel model;
	public MomentumAndImpulseController controller;
	public MomentumAndImpulseView view;

	void Start(){
		
	}
}
