using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    public bool is_retractable = false;
    public Sprite spikesRetract;
    public Sprite spikesNormal;

    private SpriteRenderer spriteRenderer;
    private Collider2D coll;
	void Start () {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;
        coll = this.GetComponent<Collider2D>();
        if (is_retractable)
            coll.enabled = false;
    }
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ennemi" && !spriteRenderer.enabled)
        {
            BaseEnnemiAI behaviour = collision.gameObject.GetComponent<BaseEnnemiAI>();
            if (behaviour != null)//&& !behaviour.invincible
                behaviour.Kill();
            spriteRenderer.enabled = true;
            
            //GameObject.Destroy(this.gameObject);
        }
        if(collision.tag == "Player" && !spriteRenderer.enabled)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
                player.Kill();

            spriteRenderer.enabled = true;
        }
    }
    public bool IsUsed()
    {
        return spriteRenderer.enabled;
    }

    public void Activate()
    {
        if(is_retractable)
        {
            coll.enabled = true;
        }
    }
    public void Retracte()
    {
        if (is_retractable)
        {
            coll.enabled = false;
        }
    }
}
