using UnityEngine;
using System.Collections;

public class AccelerationWheelRotator: MonoBehaviour {

	public WheelCollider wheelCollider;


	void Start () {
	}
	

	void Update () {
		this.transform.Rotate(Vector3.right * wheelCollider.rpm * 2 * Mathf.PI / 60.0f * Time.deltaTime * Mathf.Rad2Deg );
	}
}
