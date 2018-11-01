using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

    public Transform t_to_follow;
    private Transform t;
	void Start () {
        t = this.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = t_to_follow.position;
	}
}
