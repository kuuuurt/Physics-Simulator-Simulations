using UnityEngine;
using System.Collections;

public class MomentumAndImpulseBallCollisionListener : MomentumAndImpulseElement {


	public bool collisionStarted;
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
				collisionStarted = false;
			}
		}
	}

	void OnCollisionEnter(Collision collision){
		app.controller.sfx2.Play ();
		startTime = Time.time;
		this.collision = collision;
		collisionStarted = true;
	}
}
