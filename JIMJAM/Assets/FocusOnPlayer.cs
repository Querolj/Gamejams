using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnPlayer : MonoBehaviour {
    public Transform focus;

    private Transform t;
    private PlayerController player;
	void Start () {
        t = this.GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
	
	void Update () {
        Focus();
        if (player.IsDead)
            GameObject.Destroy(this.gameObject);
	}
    private void Focus()
    {
        t.position = focus.position;
        t.rotation = focus.rotation;

    }
}
