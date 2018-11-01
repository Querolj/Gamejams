using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnnemiBehaviour : BaseEnnemiAI {

    public int life = 4;
    public float hurt_cd = 0.4f;
    private float hurt_time;

    //private bool clignotage2 = false;
    protected override void Update()
    {
        base.Update();
        if(!is_dead)
        {
            ClignoteHurt();//Si blessé
        }
    }

    private void ClignoteHurt()
    {
        if (hurt_time > 0)
        {
            hurt_time -= Time.deltaTime;
            if (clignotage)
                rend.color = new Color32(255, 255, 255, 255);
            else
                rend.color = new Color32(255, 0, 0, 255);
            if (clignote_time <= 0)
            {
                clignotage = !clignotage;
                clignote_time = 0.1f;
            }
        }
        else
            rend.color = new Color32(255, 255, 255, 255);
    }

    public override void Kill(bool killed_by_trap = true)
    {
        if(life > 0)
        {
            if(killed_by_trap)
                source.PlayDelayed(0.1f);
            life--;
            hurt_time = hurt_cd;
            clignote_time = 0.1f;
        }
        else if (life <= 0)
        {
            base.Kill();
        }
            
    }
}
