﻿using System;
using UnityEngine;

public class CarView : VelocityElement{

	public Rigidbody rigidBody;

	void Update(){
		rigidBody.velocity = new Vector3 (0f, 0f, app.model.velocity);
	}

}


