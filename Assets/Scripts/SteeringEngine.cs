using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SteeringEngine : MonoBehaviour {
	private Rigidbody _rigitbody = null;
	private SteeringBehaviour[] behaviours = null;

	//Выбор цели курсором
	[SerializeField]
	private bool _followTheCursor = true;

	//Макс. скорость
	[SerializeField]
	[Range(1.0f, 20.0f)]
	private float maxSpeed = 5.0f;

	//Макс. сила
	[SerializeField]
	[Range(0.1f, 10.0f)]
	private float maxForce = 1.0f;

	//Вектор управляющей силы
	private Vector3 _steering = Vector3.zero;

	public float MaxSpeed {
		get { return maxSpeed; }
	}

	public float MaxForce {
		get { return maxForce; }
	}

	public bool FollowTheCursor {
		get { return _followTheCursor; }
	}

	//Текущий вектор скорости
	public Vector3 Velocity {
		get { return _rigitbody.velocity; }
		set { _rigitbody.velocity = value; }
	}

	//Вектор управляющей силы
	public Vector3 Steering {
		get { return _steering; }
		set { _steering = value; }
	}

	void Start () {
		_rigitbody = GetComponent<Rigidbody> ();
		behaviours = GetComponents<SteeringBehaviour> ();
	}

	void Update () {
		if (behaviours != null)
			SetSteering ();
	}

	//Применение сил
	public void SetSteering () {
		Steering = Vector3.zero;
		for (int i = 0; i < behaviours.Length; i++) {
			if (behaviours[i].enabled)
				Steering += behaviours [i].GetForce ();
		}
		Steering = Vector3.ClampMagnitude (Steering, MaxForce);
		_rigitbody.velocity = Vector3.ClampMagnitude (_rigitbody.velocity + Steering, MaxSpeed);
		if (_rigitbody.velocity.sqrMagnitude > 1)
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (_rigitbody.velocity), MaxSpeed * Time.deltaTime);
	}
}
