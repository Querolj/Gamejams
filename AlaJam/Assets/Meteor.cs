using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
    public Sprite[] sprites_explosion;
    public float speed_explosion = 0.05f;
    public Transform particle_t;
    public float death_time = 2f;
    public float max_velocity = 0.5f;

    private float explosion_time = 0;
    private int count_explosion = 0;
    private bool is_exploding = false;
    private SpriteRenderer rend;
    private MeteorController meteor_controller;
    private Rigidbody2D body;
    private ParticleSystem part_sys;

    //Sound
    public AudioClip[] booms;
    private AudioSource source;
    private bool played = false;
    private float revSpeed = 10f;
    private void Awake()
    {
        rend = this.GetComponent<SpriteRenderer>();
        meteor_controller = GameObject.Find("MeteorSpawner").GetComponent<MeteorController>();
        body = this.GetComponent<Rigidbody2D>();
        part_sys = this.GetComponentInChildren<ParticleSystem>();
        source = this.GetComponent<AudioSource>();
        //source.volume = 0.2f;
    }
	
	void Update () {

        

        if(body.velocity.y >= max_velocity)
        {
            Vector3 v = body.velocity;
            v.y = max_velocity;
            body.velocity = v;
        }

		if(is_exploding)
        {
            if(!played)
            {
                source.volume = 1f;
                int r = Random.Range(0, booms.Length);
                source.PlayOneShot(booms[r]);
                played = true;
            }
            if(explosion_time > 0)
            {
                explosion_time -= Time.deltaTime;
                if(explosion_time <= 0)
                {
                    if(count_explosion < sprites_explosion.Length)
                    {
                        rend.sprite = sprites_explosion[count_explosion];
                        count_explosion++;
                        explosion_time = speed_explosion;
                    }else{
                        rend.sprite = null;
                        
                    }
                }
                
            }
        }

        if (is_exploding && death_time > 0)
        {
            death_time -= Time.deltaTime;
            if(death_time <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
	}

    /*private void FixedUpdate()
    {
        if(!is_exploding)
        {
            //Rotate Meteor
            body.MoveRotation(body.rotation + revSpeed * Time.deltaTime);
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "building")
        {
            Building b = collision.gameObject.GetComponent<Building>();
            if (b != null)
                b.Damaged();
            else
            {
                BuildingReference b_ref = collision.gameObject.GetComponent<BuildingReference>();
                if (b_ref != null)
                    b_ref.Damaged();
            }
            Explose();

        }
        else if(collision.gameObject.tag == "Ovni")
        {
            Ovni o = collision.gameObject.GetComponent<Ovni>();
            if (o != null)
                o.Kill();
            else
                print("ovni null?");
            Explose();
        }
        else if(collision.gameObject.tag == "Decors" || collision.gameObject.tag == "meteor")
        {
            Explose();
        }
    }

    void Explose()
    {
        part_sys.Stop(); 
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        explosion_time = speed_explosion;
        if(count_explosion < sprites_explosion.Length)
            rend.sprite = sprites_explosion[count_explosion];
        count_explosion++;
        is_exploding = true;
        meteor_controller.RemoveMeteor(this);
    }

    //------------------------------------------------------------------------------------------------
    public void SetDirection(float angle, float speed)
    {
        //this.transform.eulerAngles = new Vector3(angle, particle_t.eulerAngles.y, particle_t.eulerAngles.z);
        if (body == null)
            print("wut");
        body.velocity = new Vector3(speed, body.velocity.y, 0);
    }
    public Transform GetTransform()
    {
        return this.transform;
    }
}