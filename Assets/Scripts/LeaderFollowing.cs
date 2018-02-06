using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Arrival))]
public class LeaderFollowing : SteeringBehaviour {
	//Расстояние от лидера до преследователей
	const float LEADER_BEHIND_DIST = 4.0f;
	//Радиус действия разделяющей силы
	const float SEPARATION_RADIUS = 5.0f;
	//Макс. сила разделения
	const float MAX_SEPARATION = 10.0f;
	//Радиус видимости лидера
	const float LEADER_SIGHT_RADIUS = 8.5f;
	//Макс. сила уклонения
	const float MAX_EVADE = 3.0f;

	//Персонаж лидера
	[SerializeField]
	private GameObject leader;

	//Компонент Arrival для управления преследователями
	private Arrival arrival;

	new protected void Start () {
		base.Start ();
		arrival = GetComponent<Arrival> ();
	}

	//Расчет сил
	public override Vector3 GetForce ()
	{
		Vector3 force = Vector3.zero;

		Vector3 tv = leader.GetComponent<Rigidbody> ().velocity;
		tv.Normalize ();
		tv *= LEADER_BEHIND_DIST;

		Vector3 ahead = leader.transform.position + tv;

		tv *= -1;

		Vector3 behind = leader.transform.position + tv;

		arrival.Target = behind;
		force = arrival.GetForce ();
		force += GetSeparationForce ();

		//Если лидер на пути, отойти в сторону
		if (IsOnLeaderSight (ahead))
			force += GetEvadeForce (leader);
			
		return force;
	}

	//Расчет разделяющей силы
	private Vector3 GetSeparationForce () {
		Vector3 force = Vector3.zero;
		int neighborCount = 0;
		GameObject[] characters = leader.GetComponent<Leader> ().Followers;

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

	//Расчет силы уклонения
	private Vector3 GetEvadeForce (GameObject enemy) {
		Vector3 force = Vector3.zero;

		Vector3 distance = enemy.transform.position - transform.position;
		float updatesAhead = distance.magnitude / Engine.MaxSpeed;
		Vector3 futurePos = enemy.transform.position + enemy.GetComponent<Rigidbody> ().velocity * updatesAhead;

		force = Vector3.Normalize (transform.position - futurePos) * Engine.MaxSpeed * MAX_EVADE;
		force.y = 0;
		force -= Engine.Velocity;

		return force;
	}

	//Расчет дистанции между векторами a и b
	private float GetDistance (Vector3 a, Vector3 b) {
		float dist = Mathf.Sqrt ((a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z));
		return dist;
	}

	//Если лидер пересекает путь, возвращает true
	private bool IsOnLeaderSight (Vector3 ahead) {
		return GetDistance(ahead, transform.position) <= LEADER_SIGHT_RADIUS || GetDistance(leader.transform.position, transform.position) <= LEADER_SIGHT_RADIUS;
	}
}
