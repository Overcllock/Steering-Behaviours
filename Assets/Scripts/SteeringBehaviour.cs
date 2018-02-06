using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringEngine))]
public abstract class SteeringBehaviour : MonoBehaviour {
	private SteeringEngine _engine;

	public SteeringEngine Engine {
		get { return _engine; }
		set { _engine = value; }
	}

	private Vector3 _target = Vector3.zero;

	//Целевая позиция
	public Vector3 Target {
		get { return _target; }
		set { _target = value; }
	}

	protected void Start () {
		_engine = GetComponent<SteeringEngine> ();
		//Target = transform.position;
	}

	void Update () {
		if (Engine.FollowTheCursor)
			ChangeTarget ();
	}

	//Смена цели
	void ChangeTarget () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity))
			Target = hitInfo.point;
	}

	//Расчет сил
	public abstract Vector3 GetForce ();
}
