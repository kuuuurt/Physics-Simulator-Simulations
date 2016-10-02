using System;
using UnityEngine;

public class VelocityGroundRecycler : VelocityElement{
	private int back;
	private Transform[] positions;

	void Start(){
		back = 1;
		positions = this.GetComponentsInChildren<Transform> ();
	}

	void Update(){
		if (app.view.car.transform.position.z - positions[back].position.z > 120) {
			Vector3 position = positions [back].position;
			positions [back].position = new Vector3 (position.x, position.y, position.z + 160);
			back += 6;
			if (back < positions.Length - 1)
				back++;
			else
				back = 1;
		}
	}
}


