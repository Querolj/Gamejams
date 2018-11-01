using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    public float speed = 2f;

    private Rigidbody2D body;
    private Player player;
    private Transform player_t;
    private Transform t;
    private Vector2 player_dir;
    private SpriteRenderer rend;
    //private float time_until_destr = 0.;
    private AudioSource source;
    private bool used = false;
	void Start () {
        player = GameObject.Find("player").GetComponent<Player>();
        player_t = GameObject.Find("player").GetComponent<Transform>();
        rend = this.GetComponent<SpriteRenderer>();
        body = this.GetComponent<Rigidbody2D>();
        t = this.GetComponent<Transform>();
        LookPlayer();
        player_dir = PlayerDirection();
        source = this.GetComponent<AudioSource>();
    }
	
	void Update () {
        body.velocity = player_dir;
        if(rend == null && !source.isPlaying)
            GameObject.Destroy(this.gameObject);
    }

    private Vector2 PlayerDirection()
    {
        float x_velo = t.position.x - player_t.position.x;
        float y_velo = t.position.y - player_t.position.y;
        float total = Mathf.Abs(x_velo) + Mathf.Abs(y_velo);
        x_velo = (x_velo / total) * speed;
        y_velo = (y_velo / total) * speed;

        return new Vector2(-x_velo, -y_velo);
    }

    private void LookPlayer()
    {
        var dir = t.position - player_t.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        t.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !used)
        {
            player.Kill(false);
            source.PlayDelayed(0.1f);
            rend.sprite = null;
            used = true;
        }
        else if (collision.tag == "ennemi" && !used)
        {
            BaseEnnemiAI behaviour = collision.gameObject.GetComponent<BaseEnnemiAI>();
            if(behaviour != null && !behaviour.is_dead)
            {
                behaviour.Kill(false);
                source.PlayDelayed(0.1f);
                rend.sprite = null;
                used = true;
            }
        }
    }
}
