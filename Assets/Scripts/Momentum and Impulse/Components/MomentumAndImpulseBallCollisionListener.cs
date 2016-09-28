using UnityEngine;
using System.Collections;

public class MomentumAndImpulseBallCollisionListener : MomentumAndImpulseElement {


	bool collisionStarted;
	float startTime;
	Collision collision;

	void Start() {
		collisionStarted = false;
	}

	void Update () {
		if (collisionStarted) {
			app.model.time = Time.time - startTime;
			if (collision.rigidbody.velocity == Vector3.zero) {
				app.controller.finished = true;
			}
		}
	}

	void OnCollisionEnter(Collision collision){
		
		startTime = Time.time;
		this.collision = collision;
		collisionStarted = true;
	}
}
