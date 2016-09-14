using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProjectileMotionView : ProjectileMotionElement {

	public CannonView cannon;
	public GameObject ground;
	public GameObject instructions;
	public ProjectileMotionStartScreenView startScreen;
	public ProjectileMotionPlayScreenView playScreen;
	public ProjectileMotionResultsView results;
	public ProjectileMotionHUDView HUD;

	void Update() {	
		if(!app.model.dontSetVelocity)
			app.view.HUD.velocityYText.text = string.Format("{0:0.00}", app.model.velocityY) + " m / s";
		app.view.HUD.heightText.text = string.Format("{0:0.00}", app.model.height) + " m";
		app.view.HUD.timeText.text = string.Format("{0:0.00}", app.model.time) + " s";
		app.view.HUD.rangeText.text = string.Format("{0:0.00}", app.model.range) + " m";
	}

}
