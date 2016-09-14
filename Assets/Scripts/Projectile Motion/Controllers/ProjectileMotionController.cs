using UnityEngine;
using UnityEngine.UI;
using System;

public class ProjectileMotionController : ProjectileMotionElement{

	GameObject projectilePrefab, projectile;
	Rigidbody projectileRG;

	bool simulate;
	bool tutorialOngoing;
	bool paused;

	float startTime;
	float previousTime;
	float previousVelocity;
	float previousVelocityX;
	float previousVelocityY;


	void Start(){
		projectilePrefab = Resources.Load("Projectile Motion/Projectile Cannon") as GameObject;
		startTutorial ();
	}

	void Update(){
		if (simulate) {
			app.model.height = projectile.transform.position.y - app.view.ground.transform.position.y - 2.6f;
			if (app.model.maxHeight < app.model.height)
				app.model.maxHeight = app.model.height;
			if (app.model.height > 0) {
				app.model.velocity = projectileRG.velocity.magnitude;
				app.model.velocityX = projectileRG.velocity.x;
				app.model.velocityY = projectileRG.velocity.y;
				app.model.range = projectile.transform.position.x - app.view.cannon.transform.position.x - 0.5f;
				if(!paused)
					app.model.time = (Time.time - startTime) + previousTime;
			} else {
				app.view.results.gameObject.SetActive (true);
				app.view.results.timeText.text = string.Format("{0:0.00}", app.model.time) + " s";
				app.view.results.heightText.text = string.Format("{0:0.00}", app.model.maxHeight) + " m";
				app.view.results.rangeText.text = string.Format("{0:0.00}", app.model.range) + " m";
				app.view.results.initialVelocityText.text = string.Format("{0:0.00}", app.view.startScreen.initialVelocity.value) + " m / s";
				app.view.results.angleText.text = string.Format("{0:0.00}", app.view.startScreen.angle.value+10) + " deg NE";
			}
		} else {
			app.model.angle = app.view.startScreen.angle.value;
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

	public void reset(){
		Destroy (projectile);
		app.view.results.gameObject.SetActive (false);
		simulate = false;
		app.view.startScreen.gameObject.SetActive (true);
		app.view.playScreen.gameObject.SetActive (false);
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
		app.model.angle = app.view.startScreen.angle.value;
		app.model.velocity = app.view.startScreen.initialVelocity.value;
		simulate = true;
		startTime = Time.time;
		fireProjectile ();
		app.view.startScreen.gameObject.SetActive (false);
		app.view.playScreen.gameObject.SetActive (true);

	}

	public void fireProjectile(){
		projectile = Instantiate (projectilePrefab) as GameObject;

		float x = Mathf.Cos ((app.model.angle+10) * Mathf.Deg2Rad) * 2.4f;
		float y = Mathf.Sin ((app.model.angle+10) * Mathf.Deg2Rad) * 2.4f;

		projectile.transform.position = app.view.cannon.transform.position + new Vector3(x, y, 0);
		projectile.transform.position = app.view.cannon.transform.position + new Vector3(2.05f, 0.85f, 0);
//		projectile.transform.rotation = app.view.cannon.transform.rotation;
		projectile.transform.RotateAround (new Vector3 (app.view.cannon.transform.position.x, app.view.cannon.transform.position.y, app.view.cannon.transform.position.z)
			, Vector3.forward, (app.model.angle + 10) - projectile.transform.eulerAngles.z);
	

		projectileRG = projectile.GetComponent<Rigidbody> ();
		app.model.velocityX = Mathf.Cos ((app.model.angle+10) * Mathf.Deg2Rad) * app.model.velocity;
		app.model.velocityY = Mathf.Sin ((app.model.angle+10) * Mathf.Deg2Rad) * app.model.velocity;
		projectileRG.velocity = new Vector3(app.model.velocityX, app.model.velocityY,0);

		app.view.playScreen.angleText.text = (app.model.angle + 10) + "deg";
		app.view.playScreen.velocityXText.text = string.Format("{0:0.00}", app.model.velocityX) + " m / s";
	}
}


