using System;
using UnityEngine;

public class WorkPowerGroundRecycler : WorkPowerElement{
	private int back;
	private Transform[] positions;

	void Start(){
		back = 1;
		positions = this.GetComponentsInChildren<Transform> ();
	}

	void Update(){
		if (app.view.cube.transform.position.x - positions[back].position.x > 17.5) {
			Vector3 position = positions [back].position;
			positions [back].position = new Vector3 (position.x + 30, position.y, position.z);
			if (back < positions.Length - 1)
				back++;
			else
				back = 1;
		}
	}
}


