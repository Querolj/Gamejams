using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingShield : MonoBehaviour {
    public AudioClip elec;
    public AudioClip shock;

    private LoopedAnime anime;
    private bool activated = false;
    private Player player;
    private AudioSource source;
    void Start () {
        anime = this.GetComponent<LoopedAnime>();
        player = this.GetComponentInParent<Player>();
        source = this.GetComponent<AudioSource>();
    }
	

    public void ActiveShield()
    {
        anime.Play();
        source.clip = elec;
        source.loop = true;
        source.Play();
        activated = true;
    }
    public void DesactiveShield()
    {
        anime.Stop();
        source.Stop();
        activated = false;
        player.shield_on = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ennemi" && activated)
        {
            print("activated");
            BaseEnnemiAI behaviour = collision.gameObject.GetComponent<BaseEnnemiAI>();
            if(behaviour != null && !behaviour.is_dead)
            {
                behaviour.Kill(false);
                DesactiveShield();
                source.clip = shock;
                source.loop = false;
                source.Play();
            }
        }
    }
}
