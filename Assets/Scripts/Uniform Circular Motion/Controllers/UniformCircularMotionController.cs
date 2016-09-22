using UnityEngine;
using UnityEngine.UI;
using System;

public class UniformCircularMotionController : UniformCircularMotionElement{

	bool simulate;
	bool tutorialOngoing;
	bool paused;
	float startTime, previousTime;
	float targetTime;

	void Start(){
		startTutorial ();
		initComponents();
	}

	void Update(){
		if (simulate) {
			if (!paused)
				app.model.time = (Time.time - startTime) + previousTime;
			app.view.HUD.timeText.text = string.Format ("{0:0.00}", app.model.time) + " s";
			if (app.model.time < targetTime) {
				app.view.satellite.transform.RotateAround (app.view.earth.transform.position, Vector3.forward, app.model.angularVelocity * Time.deltaTime);
			} else {
				simulate = false;
				app.view.results.gameObject.SetActive (true);
				app.view.results.timeText.text = string.Format ("{0:0.00}", targetTime) + " s";
				app.view.results.distanceText.text = string.Format ("{0:0.00}", app.model.distance) + " m";
				app.view.results.tangentialVelocityText.text = string.Format ("{0:0.00}", app.model.tangentialVelocity) + " m/s";
				app.view.results.angularVelocityText.text = string.Format ("{0:0.00}", app.model.angularVelocity) + " deg/s";
			}
		} else {
			app.view.startScreen.velocityValue.text = string.Format ("{0:0.00}", app.view.startScreen.velocitySlider.value) + " m/s";
			app.view.startScreen.timeValue.text = string.Format ("{0:0.00}", app.view.startScreen.timeSlider.value) + " s";
		}
	}

	public void reset(){
		app.view.results.gameObject.SetActive (false);
		simulate = false;
		app.view.startScreen.gameObject.SetActive (true);
		app.view.playScreen.gameObject.SetActive (false);
		app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
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
		
	void initComponents(){
		simulate = false;
		paused = false;
		previousTime = 0;
		startTutorial ();

	}

	public void startTutorial(){
		reset ();
		simulate = false;
		tutorialOngoing = true;
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
		
		app.model.tangentialVelocity = app.view.startScreen.velocitySlider.value;
		float circumference = 2 * Mathf.PI * app.model.earthRadius;
		targetTime = circumference / app.model.tangentialVelocity;
		app.model.angularVelocity = 360 / targetTime;

		Debug.Log (circumference);
		Debug.Log (targetTime);
		Debug.Log (app.model.tangentialVelocity);
		Debug.Log (app.model.angularVelocity);
		targetTime = app.view.startScreen.timeSlider.value;
		app.model.distance = app.model.earthRadius * (Mathf.Deg2Rad * app.model.angularVelocity * targetTime);
		simulate = true;
		startTime = Time.time;
		app.view.startScreen.gameObject.SetActive (false);
		app.view.playScreen.gameObject.SetActive (true);
	}
}


