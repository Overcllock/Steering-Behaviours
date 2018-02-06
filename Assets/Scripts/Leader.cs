using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {
	//Преследователи
	[SerializeField]
	private GameObject[] followers;

	public GameObject[] Followers {
		get { return followers; }
	}
}
