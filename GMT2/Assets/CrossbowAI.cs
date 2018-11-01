using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowAI : BaseEnnemiAI {
    public float arrow_cd = 1f;
    public GameObject arrow;

    private float arrow_time;
    private Transform launchpoint;
    private CrossbowAnimation anime_cross;
    protected override void Start()
    {
        base.Start();
        launchpoint = GameObject.Find(this.name + "/launchpoint").GetComponent<Transform>();
        anime_cross = this.GetComponent<CrossbowAnimation>();
        arrow_time = arrow_cd;
    }

    protected override void Update()
    {
        base.Update();
        if(!is_dead)
        {
            if(arrow_time > 0)
            {
                arrow_time -= Time.deltaTime;
                if (arrow_time <= 0)
                {
                    anime.launch_arrow = true;
                    block_move = true;
                }
                    //LaunchProjectile();
            }
            
        }
    }

    public void LaunchProjectile()
    {
        print("launching");
        arrow_time = arrow_cd;
        //Vector3 v = launchpoint.localPosition;
        Instantiate(arrow, launchpoint.position, Quaternion.identity);
        block_move = false;
        //Insta
    }
}
