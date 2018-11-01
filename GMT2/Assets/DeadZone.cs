using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

    private Player player;
    void Start () {
        player = GameObject.Find("player").GetComponent<Player>();

    }
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.Kill();
        }
    }
}
