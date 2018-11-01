using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnnemiAI : MonoBehaviour {
    public float speed = 1f;
    public float super_speed = 1f;
    public float super_speed_cd = 2.5f;
    [HideInInspector]
    public bool is_dead;
    [HideInInspector]
    public bool invincible = false;

    protected Transform en_t;
    private Transform player_t;
    private Rigidbody2D e_body;
    private Player player;
    [HideInInspector]
    public bool block_move = false;
    protected EnnemiAnimation anime;
    private CircleCollider2D death_collider;
    private Rules rules;
    private float super_speed_time;
    private float init_speed;
    protected SpriteRenderer rend;
    //Clignotage
    protected float clignote_time = 0.1f;
    protected bool clignotage = false;
    //Son
    protected AudioSource source;
    protected AudioClip hurt;
    //protected AudioClip arrow;
    //Collision entre ennemis
    private BoxCollider2D box;
    protected virtual void Start () {
        player_t = GameObject.Find("player").GetComponent<Transform>();
        player = GameObject.Find("player").GetComponent<Player>();
        rules = GameObject.Find("Rules").GetComponent<Rules>();
        box = this.GetComponentInChildren<BoxCollider2D>();
        rend = this.GetComponent<SpriteRenderer>();
        anime = this.GetComponent<EnnemiAnimation>();
        source = this.GetComponent<AudioSource>();
        rules.AddEnnemi();
        init_speed = speed;
        en_t = this.GetComponent<Transform>();
        e_body = this.GetComponent<Rigidbody2D>();
        //death_collider = GameObject.Find(this.name+ "/DeathCollider").GetComponentInChildren<CircleCollider2D>();
        //death_collider.enabled = false;
        LookPlayer();
    }

    // Update is called once per frame
    protected virtual void Update () {
        if(!is_dead)
        {
            LookPlayer();
            if (!block_move)
                MoveToPlayer();
            else
                e_body.velocity = new Vector3(0, 0, 0);
            if(super_speed_time > 0)
            {
                
                super_speed_time -= Time.deltaTime;
                Clignote();
                if (super_speed_time <= 0)
                {
                    DesactivateSuperSpeed();
                    rend.color = new Color32(255, 255, 255, 255);
                }
            }
        }
        
    }

    



    protected void Clignote()
    {
        if (clignote_time > 0)
        {
            clignote_time -= Time.deltaTime;
            if (clignotage)
                rend.color = new Color32(255, 255, 255, 255);
            else
                rend.color = new Color32(255, 100, 100, 255);
            if (clignote_time <= 0)
            {
                clignotage = !clignotage;
                clignote_time = 0.1f;
            }
        }
    }
    private void LookPlayer()
    {
        var dir = en_t.position - player_t.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        en_t.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void MoveToPlayer()
    {
        //e_body.MovePosition(player_t.position);
        float x_velo = en_t.position.x - player_t.position.x;
        float y_velo = en_t.position.y -player_t.position.y;
        float total = Mathf.Abs(x_velo) + Mathf.Abs(y_velo);
        x_velo = (x_velo / total)*speed;
        y_velo = (y_velo / total)* speed;
        e_body.velocity = new Vector2(-x_velo ,  -y_velo );
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player" && player.life > 0 && !is_dead && !player.shield_on)
        {
            //player.Hurted();
            player.Kill();
            block_move = true;
        }
    }
    private void DesactivateSuperSpeed()
    {
        speed = init_speed;
        anime.StopQuickMode();
    }

    //---------------------------------------------------------------------------------------------------------
    public void ActivateSuperSpeed()
    {
        super_speed_time = super_speed_cd;
        speed = super_speed;
        anime.QuickMode();
    }
    public virtual void Kill(bool killed_by_trap = true)
    {
        if(!is_dead)
        {
            is_dead = true;
            e_body.velocity = new Vector3(0, 0, 0);
            //e_body.isKinematic = true;
            rules.RemoveEnnemi();
            box.enabled = false;
            e_body.constraints = RigidbodyConstraints2D.FreezeAll;
            if (killed_by_trap)
                source.PlayDelayed(0.1f);
        }
    }
    
    


}
