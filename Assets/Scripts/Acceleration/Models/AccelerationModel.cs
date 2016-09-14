using UnityEngine;
using System.Collections;

public class AccelerationModel : AccelerationElement {

	public float accelerationRate;
	public float brakeRate;
	public float maxSpeed;

	public float velocity;
	public float time;
	public float distance;


	public float startAccelerate;

	public float targetDistance;

	public float startTime;

	public float startPos;
	public float endPos;

	public bool dontSetVelocity;


}
