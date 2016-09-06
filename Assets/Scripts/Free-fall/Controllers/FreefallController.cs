using UnityEngine;
using System;

public class FreefallController : FreefallElement{

	bool simulate;

	void Start(){
		startTutorial ();
		//Initialize components
		initComponents();
	}

	public void Update(){
		if (simulate) {
			app.model.velocity = app.view.crate.rigidBody.velocity.y;
			app.model.height = app.view.crate.transform.position.y - app.view.ground.transform.position.y - 0.5f;
		} else {
			app.model.velocity = app.view.HUD.velocitySlider.value;
			app.view.crate.rigidBody.velocity = new Vector3 (0, app.model.velocity, 0);
			app.model.height = app.view.HUD.heightSlider.value;
			app.view.ground.transform.position = new Vector3(0f, app.view.crate.transform.position.y - app.model.height, 0f);
		}
	}

	public void initComponents(){
		simulate = false;
		app.model.velocity = app.view.HUD.velocitySlider.value;
		app.view.crate.rigidBody.useGravity = false;
		app.model.height = app.view.crate.transform.position.y - app.view.ground.transform.position.y - 0.5f;
	}

	public void startTutorial(){

	}

	public void startSimulation(){
		simulate = true;
		app.model.velocity = app.view.HUD.velocitySlider.value;
		app.model.height = app.view.crate.transform.position.y - app.view.ground.transform.position.y - 0.5f;
		app.view.crate.rigidBody.useGravity = true;
	}
}


