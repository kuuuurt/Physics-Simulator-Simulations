using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class AccelerationController : AccelerationElement{



	void Start(){
		startTutorial ();
	}

	void Update(){
		if (!app.model.tutorialOngoing) {
			if (app.model.simulate) {
				if (app.model.distance < app.model.targetDistance) {
					app.model.accelerationRate = app.view.HUD.accelerationSlider.value;
					app.model.brakeRate = app.view.HUD.brakeSlider.value;
					app.model.distance = app.view.car.transform.position.z - app.model.startPos;
					app.model.time = (Time.time - app.model.startTime) + app.model.previousTime;
					if (app.model.accelerate) {
						app.model.velocity += Time.deltaTime * app.model.accelerationRate;
						if (app.model.velocity > app.model.maxSpeed) {
							app.model.velocity = app.model.maxSpeed;
							app.model.accelerate = false;
						}
						Debug.Log ("Acceleration : " + app.model.velocity / app.model.time);
					} else if (app.model.brake) {
						app.model.velocity -= Time.deltaTime * app.model.brakeRate;
						if (app.model.velocity < 0) {
							app.model.velocity = 0;
							app.model.brake = false;
						}
					}
				} else {
					app.view.HUD.buttonStop.gameObject.SetActive (false);
					app.view.HUD.buttonPause.gameObject.SetActive (false);
					app.view.results.gameObject.SetActive(true);
					app.view.results.accelerationText.text = string.Format("{0:0.00}", app.model.targetDistance / app.model.velocity) + " m / s2";
					app.view.results.distanceText.text = string.Format("{0:0.00}", app.model.targetDistance) + " m";
					app.view.results.timeText.text = string.Format("{0:0.00}", app.model.time) + " s";

					app.model.dontSetVelocity = true;
					app.model.velocity = 0f;
					app.model.simulate = false;
				}
			}
		}
	}

	public void pauseResume(){
		if (app.model.simulate) {
			app.view.HUD.buttonPause.GetComponentInChildren<Text> ().text = "Resume";
			app.model.simulate = false;
			app.model.previousTime = app.model.time;
			app.model.previousVelocity = app.model.velocity;
			app.model.dontSetVelocity = true;
			app.model.velocity = 0f;
		} else {
			app.view.HUD.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
			app.model.dontSetVelocity = false;
			app.model.simulate = true;
			app.model.velocity = app.model.previousVelocity;
			app.model.startTime = Time.time;
		}
	}

	public void reset(){
		app.view.results.gameObject.SetActive(false);
		app.model.dontSetVelocity = false;
		app.model.velocity = 0f;
		app.model.distance = 0f;
		app.model.time = 0f;
		app.model.simulate = false;
		app.view.HUD.buttonStop.gameObject.SetActive (false);
		app.view.HUD.buttonPause.gameObject.SetActive (false);
		app.view.startScreen.targetDistance.text = "";
		app.view.startScreen.gameObject.SetActive (true);
	}

	public void startSimulation(){		
		app.model.simulate = true;

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
		app.model.simulate = false;
		app.model.tutorialOngoing = true;
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

	public void validateTargetDistance(Button btnDone){
		try{
			app.model.targetDistance = float.Parse(app.view.startScreen.targetDistance.text);
			if(app.model.tutorialOngoing){
				btnDone.interactable = true;
			}
			app.view.startScreen.buttonStart.interactable = true;
		} catch (Exception ex) {
			btnDone.interactable = false;
			app.view.startScreen.buttonStart.interactable = false;
			//set error message
		}
	}

	public void stopTutorial(){
		app.model.tutorialOngoing = false;
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
		app.model.accelerate = true;
		app.model.startAccelerate = Time.time;
		app.model.brake = false;
	}

	public void stopAcceleration(){
		app.model.accelerate = false;
	}

	public void brakeCar(){
		app.model.brake = true;
		app.model.startAccelerate = Time.time;
		app.model.accelerate = false;
	}

	public void stopBraking(){
		app.model.brake = false;

	}
}


