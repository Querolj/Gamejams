using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePush : MonoBehaviour {
    public Trap[] traps;
    public SpikesRetractable spikes_retractable;
    public Sprite pushed_sprite;

    private SpriteRenderer rend;
    private bool activated = false;
    private Sprite init_sprite;
    void Start () {
        rend = this.GetComponent<SpriteRenderer>();
        if (rend != null)
            init_sprite = rend.sprite;
        else
            print("rend null");
    }
	
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if((collision.tag == "ennemi" || collision.tag == "Player") && !activated)
        {
            Push();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "ennemi" || collision.tag == "Player") && activated)
        {
            Depush();
        }

    }
    private void Push()
    {
        print("push");
        rend.sprite = pushed_sprite;
        for(int i=0; i < traps.Length;i++)
        {
            if(!traps[i].IsUsed())
                traps[i].Activate();
        }
            
        spikes_retractable.Activate();
        activated = true;
    }
    private void Depush()
    {
        print("depush");
        rend.sprite = init_sprite;
        for (int i = 0; i < traps.Length; i++)
        {
            if (!traps[i].IsUsed())
                traps[i].Retracte();
        }
            
        spikes_retractable.Desactivate();
        activated = false;
    }
}
