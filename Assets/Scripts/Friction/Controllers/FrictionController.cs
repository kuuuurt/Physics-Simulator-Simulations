using UnityEngine;
using System;

public class FrictionController : FrictionElement{
	
	bool tutorialOngoing;
	bool rotateLeft, rotateRight;

	void Start(){
		//startTutorial ();
		stopTutorial();
		//Initialize components
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
		foreach (GameObject f in app.view.floor) {
			f.GetComponent<BoxCollider> ().material.dynamicFriction = app.model.coefficient;
		}
		if(direction.Equals("right"))
			app.view.boxObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 0, app.model.force));
		else 
			app.view.boxObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 0, -app.model.force));
	}
}



