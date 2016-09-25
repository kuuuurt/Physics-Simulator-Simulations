using UnityEngine;
using UnityEngine.UI;
using System;

public class WorkPowerController : WorkPowerElement{

	bool simulate;
	bool tutorialOngoing;
	bool paused;
	float startTime, previousTime;
	float previousVelocity;
	float targetTime;
	float startX;
	Rigidbody rg;

	void Start(){
		startTutorial ();
		initComponents();
	}

	void Update(){
		if (simulate) {
			if (!paused) {
				if (rg.velocity.x > 0) {
					app.model.time = Mathf.Round(((Time.time - startTime) + previousTime) * 100) / 100;
					app.model.displacement = app.view.cube.transform.position.x - startX;
					app.view.HUD.timeText.text = string.Format ("{0:0.00}", app.model.time) + " s";
					app.view.HUD.distanceText.text = string.Format ("{0:0.00}", app.model.displacement) + " s";
				} else {
					simulate = false;
					app.view.results.gameObject.SetActive (true);
					app.view.HUD.timeText.text = string.Format ("{0:0.00}", app.model.time) + " s";
					app.view.HUD.distanceText.text = string.Format ("{0:0.00}", app.model.displacement) + " s";
					app.view.cube.GetComponent<Rigidbody> ().velocity = Vector3.zero;
					app.view.cube.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
					app.view.results.jouleText.text = string.Format ("{0:0.00}", app.model.force * app.model.displacement) + " J";
					app.view.results.powerText.text = string.Format ("{0:0.00}", (app.model.force * app.model.displacement) / app.model.time) + " W";
				}
			}
		} else {
			app.view.cube.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			app.view.cube.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		}
	}

	public void reset(){
		app.view.results.gameObject.SetActive (false);
		app.view.cube.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		app.view.cube.transform.localPosition = new Vector3(0,0.75f,0);
		simulate = false;
		paused = false;
		previousTime = 0;
		app.view.startScreen.gameObject.SetActive (true);
		app.view.playScreen.gameObject.SetActive (false);
		app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Pause";

	}

	public void pauseResume(){
		if (!paused) {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Resume";
			paused = true;
			previousTime = app.model.time;
			previousVelocity = rg.velocity.x;
			rg.velocity = Vector3.zero;
		} else {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
			paused = false;
			startTime = Time.time;
			rg.velocity = Vector3.right * previousVelocity;
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
		app.model.force = app.view.startScreen.forceSlider.value;
		app.model.angle = 45;
		app.model.force = app.model.force * Mathf.Cos (45 * Mathf.Deg2Rad);
		rg = app.view.cube.GetComponent<Rigidbody> ();
		rg.velocity = new Vector3 (0.01f, 0, 0);
		rg.AddForce (app.model.force,0,0,ForceMode.VelocityChange);

		startX = app.view.cube.transform.position.x;
		startTime = Time.time;
	
		app.view.playScreen.force.text = string.Format ("{0:0.00}", app.model.force) + " N";
		app.view.startScreen.gameObject.SetActive (false);
		app.view.playScreen.gameObject.SetActive (true);

		simulate = true;
	}

}


