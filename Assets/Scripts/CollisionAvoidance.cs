using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SteeringBehaviour {
	//Макс. сила обхода
	const float MAX_AVOID_FORCE = 8.5f;

	//Препятствующие объекты
	[SerializeField]
	private GameObject obstacles;

	private Obstacle[] obstaclesList;

	//Вектора, направленные вперед от персонажа
	private Vector3 ahead;
	private Vector3 ahead2;

	new protected void Start () {
		base.Start ();
		obstaclesList = obstacles.GetComponentsInChildren<Obstacle> ();
	}

	//Расчет сил
	public override Vector3 GetForce () {
		//Коэффициент длины вектора, зависящей от скорости движения
		float dynamicLength = Engine.Velocity.magnitude / Engine.MaxSpeed;

		ahead = transform.position + Engine.Velocity.normalized * dynamicLength;
		ahead2 = transform.position + Engine.Velocity.normalized * dynamicLength * 0.5f;

		Obstacle mostThreatening = FindMostThreateningObstacle ();
		Vector3 avoidanceForce = new Vector3 ();

		//Если на пути препятствие
		if (mostThreatening != null) {
			avoidanceForce = ahead - mostThreatening.Center;
			avoidanceForce.y = 0;
			avoidanceForce.Normalize ();
			avoidanceForce *= MAX_AVOID_FORCE;
		}

		return avoidanceForce;
	}

	//Расчет дистанции между векторами a и b
	private float GetDistance (Vector3 a, Vector3 b) {
		float dist = Mathf.Sqrt ((a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z));
		return dist;
	}

	//Проверка на коллизию
	private bool IsCollisionFound (Vector3 ahead, Vector3 ahead2, Obstacle obstacle) {
		return GetDistance (obstacle.Center, ahead) <= obstacle.Radius || GetDistance (obstacle.Center, ahead2) <= obstacle.Radius;
	}

	//Поиск ближайшего препятствия
	private Obstacle FindMostThreateningObstacle () {
		Obstacle mostThreatening = null;

		foreach (Obstacle obstacle in obstaclesList) {
			if (obstacle.Equals (GetComponent<Obstacle> ()))
				continue;
			bool collision = IsCollisionFound (ahead, ahead2, obstacle);
			if (collision) {
				if (mostThreatening == null ||
				    GetDistance (transform.position, obstacle.Center) < GetDistance (transform.position, mostThreatening.Center))
					mostThreatening = obstacle;
			}
		}

		return mostThreatening;
	}
}