using UnityEngine;
using System;

public class FrictionController : FrictionElement{
	
	bool tutorialOngoing;

	Rigidbody rg;
	public AudioSource sfx;

	void Start(){
		startTutorial ();

		//Initialize components
		rg = app.view.boxObject.GetComponent<Rigidbody> ();
	}


	void Update(){
		app.model.force = app.view.HUD.forceSlider.value;
		app.model.coefficient = app.view.HUD.frictionSlider.value;
		app.model.frictionForce = app.model.coefficient * app.model.force;
		app.model.netForce = app.model.force - app.model.frictionForce;
		app.view.HUD.forceText.text = string.Format("{0:0.00}", app.model.force) + " N";
		app.view.HUD.coefficientOfFrictionText.text = string.Format("{0:0.00}", app.model.coefficient);
		app.view.HUD.frictionForceText.text = string.Format("{0:0.00}", app.model.frictionForce) + " N";
		if (rg.velocity != Vector3.zero) {
			playFrictionSound ();
		} else {
			app.controller.sfx.Stop ();
		}
	} 

	void playFrictionSound (){
		app.controller.sfx.pitch = rg.velocity.z;
		app.controller.sfx.Play ();
	}

	public void startTutorial(){
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
		
	public void addForce(string direction){
		
		app.model.coefficient = app.view.HUD.frictionSlider.value;
		app.model.force = app.view.HUD.forceSlider.value;
		rg.velocity = Vector3.zero;
		rg.angularVelocity = Vector3.zero;
		app.view.floor.GetComponent<BoxCollider> ().material.dynamicFriction = app.model.coefficient;
		Debug.Log (Vector3.forward  * app.model.force);
		if(direction.Equals("right"))
			rg.AddForce (transform.forward * app.model.netForce, ForceMode.Force);
		else 
			rg.AddForce (-transform.forward * app.model.netForce, ForceMode.Force);
	}
}



