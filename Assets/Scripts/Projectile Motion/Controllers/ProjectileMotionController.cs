using UnityEngine;
using System;

public class ProjectileMotionController : ProjectileMotionElement{

	GameObject projectilePrefab;
	bool simulate;
	bool tutorialOngoing;


	void Start(){
		projectilePrefab = Resources.Load("Projectile Cannon") as GameObject;
		startTutorial ();

		//Initialize components
		initComponents();
	}

	void Update(){
		if (simulate) {

		} else {
			app.model.angle = app.view.startScreen.angle.value;
		}
	}

	void initComponents(){
		
	}

	public void startTutorial(){
		
	}

	public void stopTutorial(){

	}

	public void reset(){

	}

	public void pauseResume(){

	}

	public void startSimulation(){
		app.model.angle = app.view.startScreen.angle.value + 10;
		app.model.velocity = app.view.startScreen.initialVelocity.value;
		generateButton ();
	}

	public void generateButton(){
		GameObject projectile = Instantiate (projectilePrefab) as GameObject;

		float x = Mathf.Cos (app.model.angle * Mathf.Deg2Rad) * 2.4f;
		float y = Mathf.Sin (app.model.angle * Mathf.Deg2Rad) * 2.4f;

		projectile.transform.position = app.view.cannon.transform.position + new Vector3(x, y, 0);
		projectile.transform.position = app.view.cannon.transform.position + new Vector3(2.05f, 0.85f, 0);
//		projectile.transform.rotation = app.view.cannon.transform.rotation;
		projectile.transform.RotateAround (new Vector3 (app.view.cannon.transform.position.x, app.view.cannon.transform.position.y, app.view.cannon.transform.position.z)
			, Vector3.forward, app.model.angle - projectile.transform.eulerAngles.z);
	

		Rigidbody rg = projectile.GetComponent<Rigidbody> ();
		app.model.velocityX = Mathf.Cos (app.model.angle * Mathf.Deg2Rad) * app.model.velocity;
		app.model.velocityY = Mathf.Sin (app.model.angle * Mathf.Deg2Rad) * app.model.velocity;
		rg.velocity = new Vector3(app.model.velocityX, app.model.velocityY,0);
	}
}


