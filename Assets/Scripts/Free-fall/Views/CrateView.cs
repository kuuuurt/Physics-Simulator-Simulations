using System;
using UnityEngine;


public class CrateView : FreefallElement{
	public Rigidbody rigidBody;

	public void Update(){
		Debug.Log (rigidBody.velocity);
	}

}


