using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesRetractable : MonoBehaviour {
    /*
     * Juste pour les graphismes
     */ 
    public Sprite spikesRetract;
    public Sprite spikesNormal;

    private SpriteRenderer rend;
    void Start () {
        rend = this.GetComponent<SpriteRenderer>();
    }
	
	public void Activate()
    {
        rend.sprite = spikesNormal;
    }
    public void Desactivate()
    {
        rend.sprite = spikesRetract;
    }
}
