using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class AccelerationController : AccelerationElement{

	float previousTime;
	float previousVelocity;

	bool simulate;
	bool tutorialOngoing;

	bool accelerate;
	bool brake;


	void Start(){
		startTutorial ();
	}

	void Update(){
		
			if (simulate) {
				if (app.model.distance < app.model.targetDistance) {
					app.model.accelerationRate = app.view.HUD.accelerationSlider.value;
					app.model.brakeRate = app.view.HUD.brakeSlider.value;
					app.model.distance = app.view.car.transform.position.z - app.model.startPos;
					app.model.time = (Time.time - app.model.startTime) + previousTime;
					if (accelerate) {
						app.model.velocity += Time.deltaTime * app.model.accelerationRate;
						if (app.model.velocity > app.model.maxSpeed) {
							app.model.velocity = app.model.maxSpeed;
							accelerate = false;
						}
					} else if (brake) {
						app.model.velocity -= Time.deltaTime * app.model.brakeRate;
						if (app.model.velocity < 0) {
							app.model.velocity = 0;
							brake = false;
						}
					}
				} else {
					app.view.HUD.buttonStop.gameObject.SetActive (false);
					app.view.HUD.buttonPause.gameObject.SetActive (false);
					app.view.results.gameObject.SetActive (true);
					app.view.results.accelerationText.text = string.Format ("{0:0.00}", app.model.targetDistance / app.model.velocity) + " m / s2";
					app.view.results.distanceText.text = string.Format ("{0:0.00}", app.model.targetDistance) + " m";
					app.view.results.timeText.text = string.Format ("{0:0.00}", app.model.time) + " s";

					app.model.dontSetVelocity = true;
					app.model.velocity = 0f;
					simulate = false;
				}
			} else {
				app.model.targetDistance = app.view.startScreen.distanceSlider.value;
				app.view.startScreen.distanceText.text = string.Format ("{0:0.00}", app.model.targetDistance) + " m";
			}
	}

	public void pauseResume(){
		if (simulate) {
			app.view.HUD.buttonPause.GetComponentInChildren<Text> ().text = "Resume";
			simulate = false;
			previousTime = app.model.time;
			previousVelocity = app.model.velocity;
			app.model.dontSetVelocity = true;
			app.model.velocity = 0f;
		} else {
			app.view.HUD.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
			app.model.dontSetVelocity = false;
			simulate = true;
			app.model.velocity = previousVelocity;
			app.model.startTime = Time.time;
		}
	}

	public void reset(){
		app.view.results.gameObject.SetActive(false);
		app.model.dontSetVelocity = false;
		app.model.velocity = 0f;
		app.model.distance = 0f;
		app.model.time = 0f;
		simulate = false;
		app.view.HUD.buttonStop.gameObject.SetActive (false);
		app.view.HUD.buttonPause.gameObject.SetActive (false);
		app.view.startScreen.distanceText.text = "0.00 m";
		app.view.startScreen.gameObject.SetActive (true);
		app.view.HUD.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
	}

	public void startSimulation(){		
		simulate = true;

		app.view.HUD.buttonStop.gameObject.SetActive (true);
		app.view.HUD.buttonPause.gameObject.SetActive (true);
		app.model.startPos = app.view.car.transform.position.z;
		app.model.endPos = app.model.startPos + app.model.targetDistance;

		app.view.HUD.distanceSlider.minValue = 0;
		app.view.HUD.distanceSlider.maxValue = app.model.targetDistance;

		app.model.startTime = Time.time;

		app.view.startScreen.gameObject.SetActive(false);

	}

	public void startTutorial(){
		reset ();
		simulate = false;
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
		

	public void accelerateCar(){
		accelerate = true;
		app.model.startAccelerate = Time.time;
		brake = false;
	}

	public void stopAcceleration(){
		accelerate = false;
	}

	public void brakeCar(){
		brake = true;
		app.model.startAccelerate = Time.time;
		accelerate = false;
	}

	public void stopBraking(){
		brake = false;
	}
}


