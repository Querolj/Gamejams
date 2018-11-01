using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonus : MonoBehaviour {

    private Player player;
    private AudioSource source;
    private bool activated = false;
    private SpriteRenderer rend;
    void Start () {
        player = GameObject.Find("player").GetComponent<Player>();
        source = this.GetComponent<AudioSource>();
        rend = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(activated && !source.isPlaying)
            GameObject.Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !activated)
        {
            player.ActiveSuperSpeed();
            source.Play();
            activated = true;
            rend.sprite = null;
        }
        else if(collision.tag == "ennemi" && !activated)
        {
            BaseEnnemiAI behaviour = collision.gameObject.GetComponent<BaseEnnemiAI>();
            if (behaviour != null)
            {
                behaviour.ActivateSuperSpeed();
                source.Play();
                activated = true;
                rend.sprite = null;
            }
                
        }
    }
}
