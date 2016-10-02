using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorkPowerView : WorkPowerElement {
	public WorkPowerHUDView HUD;
	public WorkPowerStartScreenView startScreen;
	public WorkPowerPlayScreenView playScreen;
	public WorkPowerResultsView results;

	public GameObject cube;
	public GameObject instructions;

	void Update(){
		app.view.HUD.timeText.text = string.Format ("{0:0.00}", app.model.time) + " s";
		app.view.HUD.distanceText.text = string.Format ("{0:0.00}", app.model.displacement) + " m";
	}
}
