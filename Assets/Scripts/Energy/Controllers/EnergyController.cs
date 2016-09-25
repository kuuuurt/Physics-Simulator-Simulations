using UnityEngine;
using UnityEngine.UI;
using System;

public class EnergyController : EnergyElement{

	bool simulate;
	bool paused;
	bool finished;
	float startTime;
	float targetTime;
	float previousTime;

	void Start(){
		startTutorial ();
	}

	void Update(){
		if (simulate) {
			if (!paused) {
				if (app.model.time < 2f) {
					app.model.time = (Time.time - startTime) + previousTime;
				} else {
					simulate = false;
					finished = true;
					app.view.results.gameObject.SetActive (true);
					app.view.results.energy.text = string.Format ("{0:0.00}", (app.model.mass * Mathf.Pow(app.model.velocity, 2))/ 2) + " J";
				
				}
			}
		} else {
			if (!finished) {
				app.view.startScreen.massSliderText.text = string.Format ("{0:0.00}", app.view.startScreen.massSlider.value) + " kg";
				app.view.startScreen.velocitySliderText.text = string.Format ("{0:0.00}", app.view.startScreen.velocitySlider.value) + " m/s";
			}
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
		previousTime = 0;
		app.view.startScreen.gameObject.SetActive (true);
		app.view.playScreen.gameObject.SetActive (false);
		app.view.results.gameObject.SetActive (false);

	}

	public void startSimulation(){
		stopTutorial ();
		app.model.mass = app.view.startScreen.massSlider.value;
		app.model.velocity = app.view.startScreen.velocitySlider.value;

		app.view.bullet.GetComponent<Rigidbody> ().AddForce (app.model.velocity, 0, 0, ForceMode.VelocityChange);
		app.view.bullet.GetComponent<Rigidbody> ().useGravity = true;
		simulate = true;
		startTime = Time.time;
		app.view.startScreen.gameObject.SetActive (false);
		app.view.playScreen.gameObject.SetActive (true);

	}

	public void pauseResume(){
		if (!paused) {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Resume";
			paused = true;
			previousTime = app.model.time;

		} else {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
			paused = false;
			startTime = Time.time;
		}
	}
}


