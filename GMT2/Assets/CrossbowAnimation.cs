using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowAnimation : EnnemiAnimation {
    public Sprite[] spritesLaunchArrow;
    public float fps_arrow = 12;
    

    private int count_arrow= 0;
    private int old_index_arrow = -1;
    private CrossbowAI behaviour_cross;//Just for launching proj
    public override void Start()
    {
        base.Start();
        behaviour_cross = this.GetComponent<CrossbowAI>();
    }

    public override void Update()
    {
        if(behaviour.is_dead || !launch_arrow)
            base.Update();
        else if(launch_arrow)
        {
            int index_arrow = (int)(Time.timeSinceLevelLoad * fps_arrow);
            index_arrow = index_arrow % spritesLaunchArrow.Length;
            if (old_index_arrow != index_arrow && count_arrow < spritesLaunchArrow.Length)
            {
                spriteRenderer.sprite = spritesLaunchArrow[count_arrow];
                count_arrow++;
            }
            else if (count_arrow == spritesLaunchArrow.Length)
            {
                count_arrow = 0;
                launch_arrow = false;
                behaviour_cross.LaunchProjectile();
            }
            old_index_arrow = index_arrow;
        }

    }
}
