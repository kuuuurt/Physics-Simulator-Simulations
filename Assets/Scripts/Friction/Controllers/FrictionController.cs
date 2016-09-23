using UnityEngine;
using System;

public class FrictionController : FrictionElement{
	
	bool tutorialOngoing;

	Rigidbody rg;

	void Start(){
		//startTutorial ();
		stopTutorial();
		//Initialize components
		rg = app.view.boxObject.GetComponent<Rigidbody> ();
	}


	void Update(){
		app.view.HUD.forceText.text = app.view.HUD.forceSlider.value + " N";
		app.view.HUD.coefficientOfFrictionText.text = "" + app.view.HUD.frictionSlider.value;
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
			rg.AddForce (transform.forward * app.model.force, ForceMode.Force);
		else 
			rg.AddForce (-transform.forward * app.model.force, ForceMode.Force);
	}
}



