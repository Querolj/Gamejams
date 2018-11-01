using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiAnimation : MonoBehaviour {
    public Sprite[] spritesWalking;
    public Sprite[] spritesDying;

    public int fps_walking;
    public int fps_dying;
    protected BaseEnnemiAI behaviour;
    protected SpriteRenderer spriteRenderer;
    private int count_dying = 0;
    private int old_index_dying = -1;
    private bool def_death = false;
    private int init_fps_walking;

    //Que pour crossbow dude
    [HideInInspector]
    public bool launch_arrow = false;

    public virtual void Start () {
        init_fps_walking = fps_walking;
        behaviour = this.GetComponent<BaseEnnemiAI>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public virtual void Update () {
        int index_walking = (int)(Time.timeSinceLevelLoad * fps_walking);
        

        if(!behaviour.is_dead)
        {
            index_walking = index_walking % spritesWalking.Length;
            spriteRenderer.sprite = spritesWalking[index_walking];
        }
        else if(!def_death && behaviour.is_dead)
        {
            int index_dying = (int)(Time.timeSinceLevelLoad * fps_dying);
            index_dying = index_dying % spritesDying.Length;
            if (old_index_dying != index_dying && count_dying < spritesDying.Length)
            {
                spriteRenderer.sprite = spritesDying[count_dying];
                count_dying++;
            }
            else if(count_dying == spritesDying.Length)
            {
                count_dying = 0;
                def_death = true;
                //mort définitive
            }
            old_index_dying = index_dying;
        }
    }

    public void QuickMode()
    {
        fps_walking *= 2;
    }
    public void StopQuickMode()
    {
        fps_walking = init_fps_walking;
    }
}
