using UnityEngine;
using UnityEngine.UI;
using System;

public class EnergyController : EnergyElement{

	bool simulate;
	bool paused;
	bool finished;
	Vector3 previousVelocity;
	Rigidbody rg;
	public AudioSource sfx;

	void Start(){
		startTutorial ();
	}

	void Update(){
		if (simulate) {
			if (!paused) {
				Debug.Log (rg.velocity);
				if (rg.velocity.x <= 0) {
					simulate = false;
					app.view.results.gameObject.SetActive (true);
					app.view.playScreen.buttonStop.interactable = false;
					app.view.playScreen.buttonPause.interactable = false;
					app.view.results.energy.text = string.Format ("{0:0.00}", (app.model.mass * Mathf.Pow(app.model.velocity, 2))/ 2) + " J";
				}
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
		app.view.bullet.transform.localPosition = new Vector3 (1.845f, 1.94f, -4.975f);
		rg = app.view.bullet.GetComponent<Rigidbody> ();
		rg.velocity = Vector3.zero;
		rg.useGravity = false;
		app.view.startScreen.gameObject.SetActive (true);
		app.view.playScreen.gameObject.SetActive (false);
		app.view.results.gameObject.SetActive (false);

	}

	public void startSimulation(){
		stopTutorial ();
		app.model.mass = app.view.startScreen.massSlider.value;
		app.model.velocity = app.view.startScreen.velocitySlider.value;

		rg.velocity = new Vector3 (0.01f, 0, 0);
		rg.AddForce (app.model.velocity, 0, 0, ForceMode.VelocityChange);
		rg.useGravity = true;
		simulate = true;
		app.view.startScreen.gameObject.SetActive (false);
		app.view.playScreen.gameObject.SetActive (true);
		app.view.playScreen.buttonStop.interactable = true;
		app.view.playScreen.buttonPause.interactable = true;

		app.view.playScreen.Velocity.text = string.Format ("{0:0.00}", app.model.velocity) + " m/s";
		app.view.playScreen.Mass.text = string.Format ("{0:0.00}", app.model.mass) + " kg";

		app.controller.sfx.Play ();

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


