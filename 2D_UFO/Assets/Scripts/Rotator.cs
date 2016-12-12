using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	
	// Update is called once per frame
	void Update () { //instead of fixedUpdate because we don'T use forces
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
	}
}
