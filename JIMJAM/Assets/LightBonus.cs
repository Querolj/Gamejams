using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBonus : MonoBehaviour {

    private PlayerController player;
    private bool destroy = false;
    private AudioSource source;
    private SpriteRenderer rend;
    private GameObject light;
    void Start () {
        light = GameObject.Find(this.name + "/Light");
        rend = this.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        source = this.GetComponent<AudioSource>();
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
            player.LightModActivated = true;//sert de fct
            destroy = true;
            rend.color = new Color(1, 1, 1, 0);
            source.Play();
            light.SetActive(false);
        }
    }
}
