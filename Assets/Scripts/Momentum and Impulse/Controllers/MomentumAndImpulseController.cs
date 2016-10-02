using UnityEngine;
using UnityEngine.UI;
using System;

public class MomentumAndImpulseController : MomentumAndImpulseElement{

	bool simulate;
	bool paused;
	public bool finished;
	Rigidbody rg;
	Vector3 previousVelocity;
	public AudioSource sfx1;
	public AudioSource sfx2;

	void Start(){
		startTutorial ();
	}

	void Update(){
		if (simulate) {
			if (!paused) {
				if (finished) {
					app.view.results.gameObject.SetActive (true);

					app.view.results.momentumText.text = string.Format ("{0:0.00}", app.model.momentum) + " Ns";
					app.view.results.impulseText.text = string.Format ("{0:0.00}", app.model.impulse) + " Ns";

					rg.velocity = Vector3.zero;
					finished = false;
					app.view.playScreen.buttonStop.interactable = false;
					app.view.playScreen.buttonPause.interactable = false;
				}
//				if (app.model.time < 2f) {
//					app.model.time = (Time.time - startTime) + previousTime;
//				} else {
//					simulate = false;
//					finished = true;
//					app.view.results.gameObject.SetActive (true);
//					/app.view.results.energy.text = string.Format ("{0:0.00}", (app.model.mass * Mathf.Pow(app.model.velocity, 2))/ 2) + " J";
//
//				}
			}
		} else {
			app.view.startScreen.massSliderText.text = string.Format ("{0:0.00}", app.view.startScreen.massSlider.value) + " kg";
			app.view.startScreen.velocitySliderText.text = string.Format ("{0:0.00}", app.view.startScreen.velocitySlider.value) + " m/s";
		}
	}

	public void startTutorial(){
		reset ();
		simulate = false;
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
		app.view.startScreen.gameObject.SetActive (true);
		app.view.playScreen.gameObject.SetActive (false);
		app.view.results.gameObject.SetActive (false);
		rg = app.view.ball.GetComponent<Rigidbody> ();
		rg.velocity = Vector3.zero;

		app.view.ball.GetComponent<MomentumAndImpulseBallCollisionListener> ().collisionStarted = false;

		app.view.ball.transform.localPosition = new Vector3 (-3.376f, 1.1f, 0);
		app.view.catcher.transform.localPosition = new Vector3 (4, -0.9f, 0);
	}

	public void startSimulation(){
		stopTutorial ();
		app.model.mass = app.view.startScreen.massSlider.value;
		app.model.velocity = app.view.startScreen.velocitySlider.value;
		rg.AddForce (app.model.velocity, 0, 0, ForceMode.Impulse);
		rg.mass = app.model.mass;
		app.model.momentum = app.model.mass * app.model.velocity;
		app.model.impulse = -app.model.momentum;
		simulate = true;
		app.view.startScreen.gameObject.SetActive (false);
		app.view.playScreen.gameObject.SetActive (true);
		app.view.playScreen.buttonStop.interactable = true;
		app.view.playScreen.buttonPause.interactable = true;

		app.controller.sfx1.Play ();
	}

	public void pauseResume(){
		if (!paused) {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Resume";
			paused = true;
			previousVelocity = rg.velocity;
			rg.velocity = Vector3.zero;
		} else {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
			paused = false;
			rg.velocity = previousVelocity;
		}
	}
}



