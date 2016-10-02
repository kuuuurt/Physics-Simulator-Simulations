using UnityEngine;
using UnityEngine.UI;
using System;

public class CentripetalAndCentrifugalForcesController : CentripetalAndCentrifugalForcesElement{

	bool simulate;
	bool paused;
	bool finished;
	float startTime;
	float targetTime;
	float previousTime;
	public AudioSource sfx;


	void Start(){
		startTutorial ();
	}

	void Update(){
		if (simulate) {
			if (!paused) {
				app.model.time = (Time.time - startTime) + previousTime;
				app.view.HUD.timeText.text = string.Format ("{0:0.00}", app.model.time) + " s";
				if (app.model.time < targetTime) {
					app.view.mgr.transform.RotateAround (app.view.mgr.transform.position, Vector3.up, app.model.angularVelocity * Time.deltaTime);
				} else {
					simulate = false;
					finished = true;
					app.view.playScreen.buttonStop.interactable = false;
					app.view.playScreen.buttonPause.interactable = false;
					app.view.results.gameObject.SetActive (true);
					app.model.centripetalForce = app.model.mass * Mathf.Pow(app.model.tangentialVelocity, 2) / app.model.radius;
					app.model.centrifugalForce = -app.model.centripetalForce;
					app.view.results.centripetalForceText.text = string.Format ("{0:0.00}", app.model.centripetalForce) + " N";
					app.view.results.centrifugalForceText.text = string.Format ("{0:0.00}", app.model.centrifugalForce) + " N";
					app.controller.sfx.Stop ();
				}
			}
		} else {
			if (!finished) {
				app.view.mgr.transform.rotation = new Quaternion (0, 0, 0, 0);
				app.model.radius = app.view.startScreen.radiusSlider.value;
				app.view.human.transform.position = app.view.mgr.transform.position + new Vector3 (app.model.radius, 0, 0.67f);
				app.view.startScreen.radiusText.text = string.Format ("{0:0.00}", app.view.startScreen.radiusSlider.value) + " m";
				app.view.startScreen.timeText.text = string.Format ("{0:0.00}", app.view.startScreen.timeSlider.value) + " s";
				app.view.startScreen.massText.text = string.Format ("{0:0.00}", app.view.startScreen.massSlider.value) + " kg";
				app.view.startScreen.tangentialVelocityText.text = string.Format ("{0:0.00}", app.view.startScreen.tangentialVelocitySlider.value) + " m/s";
			}
		}
	}

	public void startTutorial(){
		reset ();
		simulate = false;
		app.model.tangentialVelocity = 0;
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
		int i = 0;
		while (true) {
			try{
				app.view.instructions.transform.GetChild (i++).gameObject.SetActive(false);
			}catch(Exception ex){
				break;
			}
		}
	}

	public void reset(){
		simulate = false;
		paused = false;
		finished = false;
		previousTime = 0;
		app.view.startScreen.gameObject.SetActive (true);
		app.view.playScreen.gameObject.SetActive (false);
		app.view.results.gameObject.SetActive (false);
		app.view.HUD.timeText.text = "0.00 s";
		if (app.controller.sfx.isPlaying)
			app.controller.sfx.Stop ();
	}

	public void startSimulation(){
		stopTutorial ();
		app.model.tangentialVelocity = app.view.startScreen.tangentialVelocitySlider.value;
		app.model.mass = app.view.startScreen.massSlider.value;
		float circumference = 2 * Mathf.PI * 4;
		targetTime = circumference / app.model.tangentialVelocity;
		app.model.angularVelocity = 360 / targetTime;
		targetTime = app.view.startScreen.timeSlider.value;
		simulate = true;
		startTime = Time.time;
		app.view.startScreen.gameObject.SetActive (false);
		app.view.playScreen.gameObject.SetActive (true);
		app.view.playScreen.buttonStop.interactable = true;
		app.view.playScreen.buttonPause.interactable = true;

		app.controller.sfx.pitch = (app.model.tangentialVelocity / 40) * 4;
		app.controller.sfx.Play ();

		app.view.playScreen.mass.text = string.Format ("{0:0.00}", app.model.mass) + " kg";
		app.view.playScreen.tangentialVelocity.text = string.Format ("{0:0.00}", app.model.tangentialVelocity) + " m/s";
		app.view.playScreen.radius.text = string.Format ("{0:0.00}", app.model.radius) + " m";

	}

	public void pauseResume(){
		if (!paused) {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Resume";
			paused = true;
			previousTime = app.model.time;
			app.controller.sfx.Pause ();
		} else {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
			paused = false;
			startTime = Time.time;
			app.controller.sfx.UnPause ();
		}
	}
}


