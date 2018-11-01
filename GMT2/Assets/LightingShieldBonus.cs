using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingShieldBonus : MonoBehaviour {

    private Player player;
	void Start () {
        player = GameObject.Find("player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !player.shield_on)
        {
            player.ActiveShield();
            GameObject.Destroy(this.gameObject);
        }
    }
}
