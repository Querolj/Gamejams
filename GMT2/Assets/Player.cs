using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int life = 3;
    public float redness_cd = 0.5f;
    public float speed = 0.5f;
    public float super_speed = 1.2f;
    public float speed_cd = 2.5f;
    [HideInInspector]
    public bool is_dead = false;
    [HideInInspector]
    public bool is_moving = false;
    [HideInInspector]
    public bool shield_on = false;

    private LifeDisplay life_display;
    private float redness_time;
    private SpriteRenderer rend;
    private Rigidbody2D body;
    private Transform t;
    private Rules rules;
    private LightingShield shield;
    
    private float speed_timer;
    private float init_speed;
    private PlayerAnimation anime;
    private bool clignotage = false; //false = normal, true = red;
    private float clignote_time = 0.05f;
    //Son
    private AudioSource source;
    private AudioClip trap_sound;
    private AudioClip shield_sound;
	void Start () {
        //life_display = GameObject.Find("Life").GetComponent<LifeDisplay>();
        rend = this.GetComponent<SpriteRenderer>();
        body = this.GetComponent<Rigidbody2D>();
        t = this.GetComponent<Transform>();
        rules = GameObject.Find("Rules").GetComponent<Rules>();
        shield = this.GetComponentInChildren<LightingShield>();
        anime = this.GetComponent<PlayerAnimation>();
        source = this.GetComponent<AudioSource>();
        init_speed = speed;
    }
	
	void Update () {
        IsDead();
        if(!is_dead)
            Move();
        if (speed_timer > 0)
        {
            speed_timer -= Time.deltaTime;
            Clignote();

            if (speed_timer <= 0)
            {
                speed = init_speed;
                anime.StopQuickMode();
                rend.color = new Color32(255, 255, 255, 255);
            }
        }
	}
    private void Clignote()
    {
        if (clignote_time > 0)
        {
            clignote_time -= Time.deltaTime;
            if (clignotage)
                rend.color = new Color32(255, 255, 255, 255);
            else
                rend.color = new Color32(255, 100, 100, 255);
            if(clignote_time <= 0)
            {
                clignotage = !clignotage;
                clignote_time = 0.1f;
            }
        }
            
    }

    private void Move()
    {
        Vector3 v = new Vector3(0, 0, 0);
        float z = 0;
        bool no_up_down = false;
        bool no_l_r = false;
        const float div = 1.7f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            v.y = 1 * speed;
            z = -90;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            v.y = -1 * speed;
            z = 90;
        }
        else
            no_up_down = true;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            v.x = -1 * speed;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                v.x /= div;
                v.y /= div;
                z = -45;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                v.x /= div;
                v.y /= div;
                z = 45;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            v.x = 1 * speed;
            z = 180;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                v.x /= div;
                v.y /= div;
                z += 45;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                v.x /= div;
                v.y /= div;
                z -= 45;
            }
        }
        else
            no_l_r = true;

        if (v.y != 0 || v.x != 0)
            is_moving = true;
        else
            is_moving = false;
        body.velocity = v;
        if (!no_up_down || !no_l_r)
            t.eulerAngles = new Vector3(0, 0, z);

    }


    private void IsDead()
    {
        if(life==0)
        {

        }
    }
    private void Redness()
    {
        if (redness_time > 0)
            redness_time -= Time.deltaTime;
        else
            rend.color = new Color(255, 255, 255);
    }
    private void GameOver()
    {
        if(!rules.HasWon())
            rules.ActivateRetryUI();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ennemi")
        {
            BaseEnnemiAI behaviour = collision.gameObject.GetComponent<BaseEnnemiAI>();
            if(behaviour != null && !behaviour.is_dead)
            {
                behaviour.block_move = true;
                Kill();
            }
            
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------
    public void Hurted()
    {
        /*if(life > 0)
        {
            life_display.Hurted();
            life--;
            redness_time = redness_cd;
            rend.color = new Color(255,50,50);
        }*/
        
    }

    public void Kill(bool killed_by_trap = true)
    {
        is_dead = true;
        is_moving = false;
        body.velocity = new Vector3(0, 0, 0);
        if (killed_by_trap)
            source.PlayDelayed(0.1f);
        GameOver();
    }

    public void ActiveShield()
    {
        shield.ActiveShield();
        shield_on = true;
    }

    public void ActiveSuperSpeed()
    {
        speed = super_speed;
        speed_timer = speed_cd;
        anime.QuickMode();
    }

}
