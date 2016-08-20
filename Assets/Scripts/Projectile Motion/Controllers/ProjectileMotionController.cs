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
			float angle = (float)Math.Abs(app.view.cannon.transform.eulerAngles.x -  360);
		
			
			//projectile.transform.Rotate(new Vector3 (0, 0, app.view.cannon.transform.eulerAngles.x + 9.25f));
		
//			while (angle > 90)
//				angle -= 90;
//
//			while (angle < -90)
//				angle += 90;
			

			float[] x = new float[2];
			float[] y = new float[2];
			Debug.Log (Mathf.Cos ((90 - angle) * Mathf.Deg2Rad));
			x [0] = Mathf.Cos (angle * Mathf.Deg2Rad) * 1.9f;
			y [0] = Mathf.Sin (angle * Mathf.Deg2Rad) * 1.9f;
			x [1] = Mathf.Cos ((90 - angle) * Mathf.Deg2Rad) * -1.27f;
			y [1] = Mathf.Sin ((90 - angle) * Mathf.Deg2Rad) * 1.27f;

			Debug.Log (x [0] + " " + x [1]);
			Debug.Log (y [0] + " " + y [1]);

			projectile.transform.position = app.view.cannon.transform.position + new Vector3(x[0] + x[1], y[0] + y[1], 0);

			Rigidbody rg = projectile.GetComponent<Rigidbody> ();
			rg.AddForce(new Vector3(app.view.cannon.transform.eulerAngles.x, app.view.cannon.transform.eulerAngles.y,0));

		}
	}

	void initComponents(){

	}

	void startTutorial(){

	}
}


