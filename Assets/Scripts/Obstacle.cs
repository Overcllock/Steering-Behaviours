using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	[SerializeField][Range(0.0f, 30.0f)]
	private float radius;

	//Центр препятствия
	public Vector3 Center {
		get { return new Vector3(transform.position.x, 1.0f, transform.position.z); }
	}

	//Радиус препятствия
	public float Radius {
		get { return radius; }
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}
