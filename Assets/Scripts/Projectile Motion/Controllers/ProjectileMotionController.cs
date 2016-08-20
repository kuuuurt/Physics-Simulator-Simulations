using UnityEngine;
using System;

public class ProjectileMotionController : ProjectileMotionElement{

	GameObject projectilePrefab;

	void Start(){
		projectilePrefab = Resources.Load("Projectile Cannon") as GameObject;
		startTutorial ();
		//Initialize components
		initComponents();
	}

	void Update(){
		if (Input.GetMouseButtonUp (0)) {
			GameObject projectile = Instantiate (projectilePrefab) as GameObject;
			Debug.Log ("Cannon : " + app.view.cannon.transform.rotation.eulerAngles.x);
			Debug.Log ("Local : " + app.view.cannon.transform.rotation.x);
			Debug.Log ("Cannon : " + (app.view.cannon.transform.rotation.eulerAngles.x - 270));
			Debug.Log ("Projectile : " + (projectile.transform.rotation.eulerAngles.z));
			projectile.transform.position = app.view.cannon.transform.position + new Vector3(float.Parse((Math.Sin(projectile.transform.rotation.eulerAngles.y) * 1.78).ToString()), float.Parse((Math.Cos(projectile.transform.rotation.eulerAngles.y) * 1.78).ToString()) ,0);
			///Rigidbody rg = projectile.GetComponent<Rigidbody> ();
			//rg.AddForce (-app.view.cannon.transform.up * 500);
		}
	}

	void initComponents(){

	}

	void startTutorial(){

	}
}


