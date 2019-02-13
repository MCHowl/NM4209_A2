using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {
	public float destroyDelay;

    void Start() {
		Destroy(this.gameObject, destroyDelay);
    }
}
