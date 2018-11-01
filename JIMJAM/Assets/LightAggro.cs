using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAggro : MonoBehaviour {


    private PlayerController player;
    private int fish_count = 0;
    void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

    }
	
	void Update () {
		if(fish_count > 0)
        {
            player.SetCircleAlpha(1);
        }
        else
            player.SetCircleAlpha(0.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ennemi")
        {
            NormalFish fish = collision.gameObject.GetComponent<NormalFish>();
            if(fish != null)
            {
                fish.NewFocus(player.GetPosition());
                fish_count++;
            }
                
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ennemi")
        {
            NormalFish fish = collision.gameObject.GetComponent<NormalFish>();
            if (fish != null)
            {
                fish.NoFocus();
                fish_count--;
            }
                
        }
    }
}
