using UnityEngine;
using UnityEngine.UI;
using System;

public class FreefallController : FreefallElement{

	bool simulate;
	bool tutorialOngoing;
	bool paused;

	float startTime;
	float previousTime;
	float previousVelocity;

	void Start(){
		startTutorial ();
		//Initialize components
		initComponents();
	}

	public void Update(){
		if (simulate) {
			if (!paused) {
				if (app.model.height > 0) {
					app.model.velocity = app.view.crate.rigidBody.velocity.y;
					app.model.height = app.view.crate.transform.position.y - app.view.ground.transform.position.y - 0.5f;
					app.model.time = (Time.time - startTime) + previousTime;

				} else {
					app.view.playScreen.buttonStop.interactable = false;
					app.view.playScreen.buttonPause.interactable = false;
					app.view.results.gameObject.SetActive(true);
					app.view.results.timeText.text = string.Format("{0:0.00}", app.model.time) + " s";
					app.view.results.heightText.text = string.Format("{0:0.00}", app.view.startScreen.heightSlider.value) + " m";
					app.view.results.velocityText.text = string.Format("{0:0.00}", app.view.startScreen.velocitySlider.value) + " m / s";

					app.model.dontSetVelocity = true;
					app.model.velocity = 0f;
				}
			}
		} else {
			app.model.velocity = app.view.startScreen.velocitySlider.value;
			app.model.height = app.view.startScreen.heightSlider.value;
			app.view.crate.transform.position = new Vector3 (0f, app.view.ground.transform.position.y + app.model.height, 0f);
			
		}
	}

	public void initComponents(){
		simulate = false;
		app.model.velocity = app.view.startScreen.velocitySlider.value;
		app.view.crate.rigidBody.useGravity = false;
		app.model.height = app.view.crate.transform.position.y - app.view.ground.transform.position.y - 0.5f;

	}

	public void startTutorial(){
		reset ();
		simulate = false;
		tutorialOngoing = true;
		app.model.velocity = 0;
		int i = 1;
		while (true) {
			try{
				app.view.instructions.transform.GetChild (i++).gameObject.SetActive(false);
			}catch(Exception ex){
				break;
			}
		}
		app.view.instructions.transform.GetChild (0).gameObject.SetActive(true);
	}

	public void stopTutorial(){
		tutorialOngoing = false;
		int i = 0;
		while (true) {
			try{
				app.view.instructions.transform.GetChild (i++).gameObject.SetActive(false);
			}catch(Exception ex){
				break;
			}
		}
	}

	public void startSimulation(){
		stopTutorial ();
		simulate = true;
		startTime = Time.time;
		app.model.velocity = app.view.startScreen.velocitySlider.value;
		app.view.crate.rigidBody.velocity = new Vector3(0, app.model.velocity, 0);
		app.model.height = app.view.crate.transform.position.y - app.view.ground.transform.position.y - 0.5f;
		app.view.crate.rigidBody.useGravity = true;
		app.view.startScreen.gameObject.SetActive (false);
		app.view.playScreen.gameObject.SetActive (true);
		app.view.playScreen.buttonStop.interactable = true;
		app.view.playScreen.buttonPause.interactable = true;
	}

	public void reset(){
		app.view.results.gameObject.SetActive(false);
		app.model.dontSetVelocity = false;
		app.model.velocity = 0f;
		app.model.height = 0f;
		app.model.time = 0f;
		simulate = false;
		paused = false;
		app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
		app.view.startScreen.gameObject.SetActive (true);
		app.view.playScreen.gameObject.SetActive (false);
	}

	public void pauseResume(){
		if (!paused) {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Resume";
			paused = true;
			previousTime = app.model.time;
			previousVelocity = app.model.velocity;
			app.model.dontSetVelocity = true;
			app.model.velocity = 0f;
			app.view.crate.rigidBody.useGravity = false;
			app.view.crate.rigidBody.velocity = new Vector3 (0, 0, 0);
		} else {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
			app.model.dontSetVelocity = false;
			paused = false;
			app.model.velocity = previousVelocity;
			app.view.crate.rigidBody.velocity = new Vector3 (0, app.model.velocity, 0);
			app.view.crate.rigidBody.useGravity = true;
			startTime = Time.time;
		}
	}

}


