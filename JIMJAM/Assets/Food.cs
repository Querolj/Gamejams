using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    public float grow = 0.5f;

    private PlayerController player;
    private Rules rules;
    private AudioSource source;
    private bool destroy =false;
    private SpriteRenderer rend;
    void Start () {
        rend = this.GetComponent<SpriteRenderer>();
        source = this.GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        rules = this.GetComponentInParent<Rules>();
    }

    private void Update()
    {
        if(destroy && !source.isPlaying)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !destroy)
        {
            player.Grow(grow);
            rules.LessFood();
            source.Play();
            rend.color = new Color(1, 1, 1, 0);
            destroy = true;
        }
    }
}
