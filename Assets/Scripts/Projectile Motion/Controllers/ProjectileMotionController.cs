using UnityEngine;
using UnityEngine.UI;
using System;

public class ProjectileMotionController : ProjectileMotionElement{

	Rigidbody projectileRG;

	bool simulate;
	bool tutorialOngoing;
	bool paused;

	float startTime;
	float previousTime;
	float previousVelocity;
	float previousVelocityX;
	float previousVelocityY;

	public AudioSource sfx;


	void Start(){
		startTutorial ();
	}

	void Update(){
		if (simulate) {
			if (!paused) {
				app.model.height = app.view.cannonBall.transform.position.y - app.view.ground.transform.position.y - 7.6f;
				if (app.model.maxHeight < app.model.height)
					app.model.maxHeight = app.model.height;
				Debug.Log (app.model.height);
				if (app.model.height > 0.02f) {
					app.model.velocity = projectileRG.velocity.magnitude;
					app.model.velocityX = projectileRG.velocity.x;
					app.model.velocityY = projectileRG.velocity.y;
					app.model.range = app.view.cannonBall.transform.position.x - app.view.cannon.transform.position.x - 0.5f;

					app.model.time = (Time.time - startTime) + previousTime;
				} else {
					projectileRG.velocity = Vector3.zero;
					app.view.playScreen.buttonStop.interactable = false;
					app.view.playScreen.buttonPause.interactable = false;
					app.view.results.gameObject.SetActive (true);
					app.view.results.timeText.text = string.Format ("{0:0.00}", app.model.time) + " s";
					app.view.results.heightText.text = string.Format ("{0:0.00}", app.model.maxHeight) + " m";
					app.view.results.rangeText.text = string.Format ("{0:0.00}", app.model.range) + " m";
					app.view.results.initialVelocityText.text = string.Format ("{0:0.00}", app.view.startScreen.initialVelocity.value) + " m / s";
					app.view.results.angleText.text = string.Format ("{0:0.00}", app.view.startScreen.angle.value + 10) + " deg NE";
				}
			}
		} else {
			app.model.angle = app.view.startScreen.angle.value;
			app.model.velocity = app.view.startScreen.initialVelocity.value;
			app.view.startScreen.angleText.text = string.Format ("{0:0.00}", app.model.angle + 10) + " deg";
			app.view.startScreen.initialVelocityText.text = string.Format ("{0:0.00}", app.model.velocity) + " m/s";
			app.view.cannonBall.transform.rotation = new Quaternion (0, 0, 0, 0.6f);

		}
	}

	public void startTutorial(){
		reset ();
		simulate = false;
		tutorialOngoing = true;
		app.model.velocity = 0;
		app.model.velocityX = 0;
		app.model.velocityY = 0;
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

	void resetProjectile(){
		projectileRG = app.view.cannonBall.GetComponent<Rigidbody> ();
		projectileRG.velocity = Vector3.zero;
		projectileRG.useGravity = false;
		app.view.cannonBall.transform.localPosition = new Vector3 (0, 1.345f, 1.87f);
		app.view.cannonBall.transform.localRotation = new Quaternion (-0.1f, -0.7f, 0.1f, 0.7f);
		app.view.camera.transform.localRotation = new Quaternion (0, 0, 0, 0);
	}

	public void reset(){
		resetProjectile ();
		app.view.results.gameObject.SetActive (false);
		simulate = false;
		app.model.range = 0;
		app.model.time = 0;
		app.model.velocityY = 0;
		app.view.startScreen.gameObject.SetActive (true);
		app.view.playScreen.gameObject.SetActive (false);
		app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
	}

	public void pauseResume(){
		if (!paused) {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Resume";
			paused = true;
			previousTime = app.model.time;
			previousVelocity = app.model.velocity;
			previousVelocityY = app.model.velocityY;
			previousVelocityX = app.model.velocityX;
			app.model.dontSetVelocity = true;
			app.model.velocity = 0f;
			projectileRG.useGravity = false;
			projectileRG.velocity = new Vector3 (0, 0, 0);
		} else {
			app.view.playScreen.buttonPause.GetComponentInChildren<Text> ().text = "Pause";
			app.model.dontSetVelocity = false;
			paused = false;
			app.model.velocity = previousVelocity;
			app.model.velocityX = previousVelocityX;
			app.model.velocityY = previousVelocityY;
			projectileRG.velocity = new Vector3 (app.model.velocityX, app.model.velocityY, 0);
			projectileRG.useGravity = true;
			startTime = Time.time;
		}
	}

	public void startSimulation(){
		stopTutorial ();
		app.model.angle = app.view.startScreen.angle.value;
		app.model.velocity = app.view.startScreen.initialVelocity.value;
		simulate = true;
		startTime = Time.time;
		fireProjectile ();
		app.view.startScreen.gameObject.SetActive (false);
		app.view.playScreen.gameObject.SetActive (true);
		app.view.playScreen.buttonStop.interactable = true;
		app.view.playScreen.buttonPause.interactable = true;
		app.controller.sfx.Play ();
	}

	public void fireProjectile(){

		float x = Mathf.Cos ((app.model.angle+10) * Mathf.Deg2Rad) * 2.4f;
		float y = Mathf.Sin ((app.model.angle+10) * Mathf.Deg2Rad) * 2.4f;


		app.model.velocityX = Mathf.Cos ((app.model.angle+10) * Mathf.Deg2Rad) * app.model.velocity;
		app.model.velocityY = Mathf.Sin ((app.model.angle+10) * Mathf.Deg2Rad) * app.model.velocity;
		projectileRG.velocity = new Vector3(app.model.velocityX, app.model.velocityY,0);
		projectileRG.useGravity = true;

		app.view.playScreen.angleText.text = (app.model.angle + 10) + "deg";
		app.view.playScreen.velocityXText.text = string.Format("{0:0.00}", app.model.velocityX) + " m / s";
	}
}




