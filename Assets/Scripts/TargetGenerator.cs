using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour {
	[SerializeField]
	private GameObject targetPrefab;

	[SerializeField]
	private Vector3 range;

	private GameObject target = null;

	//Генерация целевой позиции
	public Vector3 GenerateTarget () {
		Vector3 position = new Vector3 (Random.Range (-range.x, range.x), 1.0f, Random.Range (-range.z, range.z));
		target = Instantiate (targetPrefab, position, new Quaternion ());
		return position;
	}

	//Уничтожение текущей цели
	public void DestroyTarget () {
		if (target != null)
			Destroy (target);
	}
}
