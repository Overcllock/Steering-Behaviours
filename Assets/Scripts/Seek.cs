using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour {
	//Вектор скорости, направленный к цели
	private Vector3 desiredVelocity = Vector3.zero;

	//Расчет сил
	public override Vector3 GetForce () {
		desiredVelocity = Vector3.Normalize (Target - transform.position) * Engine.MaxSpeed;
		desiredVelocity.y = 0;
		return desiredVelocity - Engine.Velocity;
	}
}
