using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

	void Start () {
        offset.x = transform.position.x - player.transform.position.x;
        offset.z = 10;
        offset.y = -2.18f;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position - offset;
	}
}
