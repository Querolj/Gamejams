using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEvent : MonoBehaviour {

    private PlayerController player;
	void Start () {
        player = this.GetComponentInParent<PlayerController>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ennemi")
        {
            player.IsDead = true;
        }
        if (collision.tag == "obstacle")
        {
            player.SetVelocity(new Vector3(0, 0, 0));
        }
    }
}
