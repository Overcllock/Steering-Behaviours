using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrival : SteeringBehaviour {
	//Вектор скорости, направленный к цели
	private Vector3 desiredVelocity = Vector3.zero;

	//Радиус торможения
	[SerializeField][Range(5.0f, 20.0f)]
	private float slowdownRadius = 10.0f;

	//Расчет сил
	public override Vector3 GetForce () {
		float distanceToTarget = Vector3.Magnitude (Target - transform.position);
		if (distanceToTarget < slowdownRadius)
			desiredVelocity = Vector3.Normalize (Target - transform.position) * Engine.MaxSpeed * Mathf.Clamp(distanceToTarget / slowdownRadius, 0.0f, 1.0f);
		else
			desiredVelocity = Vector3.Normalize (Target - transform.position) * Engine.MaxSpeed;
		return desiredVelocity - Engine.Velocity;
	}
}
