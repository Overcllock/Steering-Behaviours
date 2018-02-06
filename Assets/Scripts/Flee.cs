using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehaviour {
	//Вектор скорости, направленный к цели
	private Vector3 desiredVelocity = Vector3.zero;

	//Расчет сил
	public override Vector3 GetForce () {
		desiredVelocity = Vector3.Normalize (transform.position - Target) * Engine.MaxSpeed;
		desiredVelocity.y = 0;
		return desiredVelocity - Engine.Velocity;
	}
}
