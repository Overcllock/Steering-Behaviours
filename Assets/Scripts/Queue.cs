using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seek))]
[RequireComponent(typeof(CollisionAvoidance))]
public class Queue : SteeringBehaviour {
	//Макс. дистанция до персонажа
	const float MAX_QUEUE_AHEAD = 3.0f;
	//Макс. радиус торможения
	const float MAX_QUEUE_RADIUS = 2.6f;
	//Радиус действия разделяющей силы
	const float SEPARATION_RADIUS = 3.1f;
	//Макс. сила разделения
	const float MAX_SEPARATION = 6.0f;

	//Эффективность торможения (чем меньше, тем сильнее)
	[SerializeField][Range(0.1f, 0.9f)]
	private float brakeEfficiency = 0.8f;

	//Эффективность экстренного торможения
	[SerializeField][Range(0.05f, 0.5f)]
	private float extremeBrakeEfficiency = 0.3f;

	//Целевая позиция
	[SerializeField]
	private Transform target;

	//Участники движения
	[SerializeField]
	private GameObject[] characters;

	new protected void Start () {
		base.Start ();
		Seek seeking = GetComponent<Seek> ();
		seeking.Target = target.position;
	}

	//Расчет сил
	public override Vector3 GetForce ()
	{
		Vector3 brakeForce = Vector3.zero;
		GameObject neighbor = GetNeighborAhead ();

		//Если впереди помеха, применить торможение
		if (neighbor != null) {
			brakeForce.x = -Engine.Steering.x * brakeEfficiency;
			brakeForce.z = -Engine.Steering.z * brakeEfficiency;

			brakeForce += Engine.Velocity * -1;
			brakeForce += GetSeparationForce ();

			//Если помеха слишком близко, применить экстренное торможение
			if (GetDistance (transform.position, neighbor.transform.position) <= MAX_QUEUE_RADIUS)
				Engine.Velocity *= extremeBrakeEfficiency;
		}

		return brakeForce;
	}

	//Расчет разделяющей силы
	private Vector3 GetSeparationForce () {
		Vector3 force = Vector3.zero;
		int neighborCount = 0;

		//Подсчет кол-ва персонажей в радиусе
		for (int i = 0; i < characters.Length; i++) {
			Vector3 characterPos = characters [i].transform.position;
			if (!characters [i].Equals (gameObject) && GetDistance (characterPos, transform.position) <= SEPARATION_RADIUS) {
				force.x += characterPos.x - transform.position.x;
				force.z += characterPos.z - transform.position.z;
				neighborCount++;
			}
		}

		if (neighborCount > 0) {
			force.x /= neighborCount;
			force.z /= neighborCount;
			force *= -1;
		}

		force.Normalize ();
		force *= MAX_SEPARATION;

		return force;
	}

	//Получить впереди идущего персонажа
	private GameObject GetNeighborAhead () {
		Vector3 queueAhead = Engine.Velocity.normalized * MAX_QUEUE_AHEAD;
		Vector3 ahead = transform.position + queueAhead;

		for (int i = 0; i < characters.Length; i++) {
			if (characters [i] != gameObject && GetDistance (ahead, characters [i].transform.position) <= MAX_QUEUE_RADIUS)
				return characters [i];
		}

		return null;
	}

	//Расчет дистанции между векторами a и b
	private float GetDistance (Vector3 a, Vector3 b) {
		float dist = Mathf.Sqrt ((a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z));
		return dist;
	}

}
