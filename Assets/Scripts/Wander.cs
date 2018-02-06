using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : SteeringBehaviour {
	//Радиус окружности перед персонажем
	[SerializeField]
	private float circleRadius = 6.0f;

	//Дистанция от персонажа до окружности
	[SerializeField]
	private float circleDistance = 10.0f;

	//Начальный угол поворота
	private float wanderAngle = 30.0f;

	//Шаг изменения угла поворота
	const float ANGLE_CHANGE = 10.0f;

	//Расчет сил
	public override Vector3 GetForce () {
		Vector3 circleCenter = new Vector3(Engine.Velocity.x, Engine.Velocity.y, Engine.Velocity.z);
		circleCenter.Normalize ();
		circleCenter *= circleDistance;

		Vector3 displacementForce = new Vector3 (0, 0, -1);
		displacementForce *= circleRadius;
		displacementForce = SetAngle (displacementForce, wanderAngle);

		wanderAngle += Random.Range (-ANGLE_CHANGE, ANGLE_CHANGE);
		return circleCenter + displacementForce;
	}

	//Поворот
	private Vector3 SetAngle (Vector3 vector, float value) {
		Vector3 result = new Vector3 (vector.x, vector.y, vector.z);
		float length = vector.magnitude;
		result.x = Mathf.Cos (value) * length;
		result.z = Mathf.Sin (value) * length;
		return result;
	}
}
