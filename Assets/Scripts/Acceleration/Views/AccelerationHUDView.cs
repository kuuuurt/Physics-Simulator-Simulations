using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccelerationHUDView : AccelerationElement{

	public Text distanceText;
	public Text velocityText;
	public Text timeText;
	public Text accelerationSliderText;
	public Text brakeSliderText;

	public Slider distanceSlider;
	public Slider accelerationSlider;
	public Slider brakeSlider;

	public Button buttonStop;
	public Button buttonPause;
}

