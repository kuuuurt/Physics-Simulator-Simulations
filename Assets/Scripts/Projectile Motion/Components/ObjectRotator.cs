using UnityEngine;
using System.Collections;

public class ObjectRotator : ProjectileMotionElement {

//	void Update () {
//		transform.RotateAround (new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z), Vector3.forward, app.model.angle);
//		//transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, new Vector3(0,0,app.model.angle), Time.deltaTime * 0.5f);
//	}

	Vector3 Pivot;
	public float xOffset;
	public float yOffset;
	public float zOffset;


	public bool rotateZ, rotateX, rotateY;

	void FixedUpdate()
	{
		if (rotateZ) {
			transform.RotateAround (Pivot, Vector3.forward, app.model.angle - transform.eulerAngles.z);
		} else if (rotateX) {
			transform.RotateAround (Pivot, -Vector3.forward, -(app.model.angle + transform.eulerAngles.x));
		} else if (rotateY) {
			transform.RotateAround (Pivot, Vector3.forward, app.model.angle - transform.eulerAngles.y);
		}
	}

	void Start(){
		Pivot = new Vector3 (transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z + zOffset);
	}

}
