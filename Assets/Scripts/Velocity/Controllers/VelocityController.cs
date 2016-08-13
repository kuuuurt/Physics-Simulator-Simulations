using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class VelocityController : VelocityElement{

	private bool simulate;
	private bool tutorialOngoing;

	void Start(){
		startTutorial ();
	}

	void Update(){
		if (!tutorialOngoing) {
			if (simulate) {
				if (app.model.distance < app.model.targetDistance) {
					app.model.velocity = app.view.HUD.velocitySlider.value;
					app.model.distance = app.view.car.transform.position.z - app.model.startPos;
					app.model.time = Time.time - app.model.startTime;
				} else {
					app.model.velocity = 0f;
					//show results here
					app.view.HUD.buttonStop.gameObject.SetActive (false);
					app.view.results.gameObject.SetActive(true);
					app.view.results.velocityText.text = string.Format("{0:0.00}", app.model.targetDistance / app.model.time) + " m / s";
					app.view.results.distanceText.text = string.Format("{0:0.00}", app.model.targetDistance) + " m";
					app.view.results.timeText.text = string.Format("{0:0.00}", app.model.time) + " s";
					simulate = false;
				}
			}
		}
	}

	public void reset(){
		app.model.velocity = 0f;
		app.model.distance = 0f;
		app.model.time = 0f;
		simulate = false;
		app.view.HUD.buttonStop.gameObject.SetActive (false);
		app.view.startScreen.targetDistance.text = "";
		app.view.startScreen.gameObject.SetActive (true);
	}

	public void startSimulation(){		
		simulate = true;

		app.view.HUD.buttonStop.gameObject.SetActive (true);
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

	public void validateTargetDistance(Button btnDone){
		try{
			app.model.targetDistance = float.Parse(app.view.startScreen.targetDistance.text);
			if(tutorialOngoing){
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

}


