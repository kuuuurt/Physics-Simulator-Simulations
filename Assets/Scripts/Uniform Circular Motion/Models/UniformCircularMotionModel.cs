using UnityEngine;
using System.Collections;

public class UniformCircularMotionModel : UniformCircularMotionElement {
	public float earthRadius = 6371000;

	public float radius;
	public float tangentialVelocity;
	public float angularVelocity;
	public float time;
	public float distance;
}
