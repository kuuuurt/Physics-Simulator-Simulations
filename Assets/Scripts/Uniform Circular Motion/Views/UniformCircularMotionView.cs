using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UniformCircularMotionView : UniformCircularMotionElement {

	public UniformCircularMotionHUDView HUD;
	public UniformCircularMotionStartScreenView startScreen;
	public UniformCircularMotionPlayScreenView playScreen;
	public UniformCircularMotionResultsView results;
	public GameObject satellite;
	public GameObject earth;

	public GameObject instructions;

	void Update(){
		app.view.HUD.timeText.text = string.Format ("{0:0.00}", app.model.time) + " s";
		app.view.HUD.distanceText.text = string.Format ("{0:0.00}", app.model.time * app.model.tangentialVelocity) + " m";
	}
}
