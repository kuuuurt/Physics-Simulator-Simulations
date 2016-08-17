using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccelerationView : AccelerationElement {

	public AccelerationCarView car;
	public AccelerationStartScreenView startScreen;
	public AccelerationHUDView HUD;
	public AccelerationResultsView results;



	public GameObject instructions;

	void Update() {
		//bind data to UI
		app.view.HUD.velocityText.text = string.Format("{0:0.00}", app.view.HUD.velocitySlider.value) + "m / s";
		app.view.HUD.distanceText.text = string.Format("{0:0.00}", app.model.distance) + "m";
		app.view.HUD.timeText.text = string.Format("{0:0.00}", app.model.time) + "s";

		app.view.HUD.distanceSlider.value = app.model.distance;
	}

}
