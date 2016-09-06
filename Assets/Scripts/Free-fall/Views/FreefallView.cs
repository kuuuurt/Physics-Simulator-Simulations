using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FreefallView : FreefallElement {
	public CrateView crate;
	public Transform ground;
	public FreefallHUDView HUD;

	void Update() {
		app.view.HUD.velocityText.text = string.Format("{0:0.00}", app.model.velocity) + " m / s";
		app.view.HUD.heightText.text = string.Format("{0:0.00}", app.model.height) + " m / s";
	}

}
