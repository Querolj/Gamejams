using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float speed = 0.5f;
    public float max_speed;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    public float threshold_speed = 100f;
    public float interval_jump_cd = 0.3f;
    public float jump_prediction = 0.5f;
    public float loading_cd = 0.35f;//Press Space during x seconds before start charging
    public float rota_speed = 5;
    public bool has_light = false;

    private SpriteRenderer rend;
    private Rigidbody2D body;
    private Transform t;
    private PlayerAnimation anime;
    private float init_speed;
    private bool is_dead = false;
    private float current_euler_angle = 0;
    private Vector3 direction;
    private float count_deceler = 1;
    private float interval_jump_time = 0;
    private bool registered_input_jump = false;
    private float loading_time = 0;
    private ParticleSystem bubbles;
    private ParticleSystem little_bubbles;
    private SpriteRenderer target;
    //Light Mod
    private bool light_mod_activated = false;
    private GameObject light;
    private SpriteRenderer circle_rend;
    void Start () {
        init_speed = speed;
        rend = this.GetComponent<SpriteRenderer>();
        body = this.GetComponent<Rigidbody2D>();
        t = this.GetComponent<Transform>();
        direction = new Vector3(0, 1, 0);
        anime = this.GetComponent<PlayerAnimation>();
        bubbles = GameObject.Find(this.name + "/BubbleSystem").GetComponent<ParticleSystem>();
        little_bubbles = GameObject.Find(this.name + "/LittleBubbleSystem").GetComponent<ParticleSystem>();
        light = GameObject.Find(this.name + "/Light");
        circle_rend = GameObject.Find(this.name + "/Light/circle_light").GetComponent<SpriteRenderer>();
        circle_rend.color = new Color(1, 1, 1, 0.3f);
        if (!has_light)
            light.SetActive(false);
        else
            LightModActivated = true;
        current_euler_angle = t.eulerAngles.z;
        //target = GameObject.Find(this.name + "/LineTarget").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        
        if(interval_jump_time > 0)
        {
            interval_jump_time -= Time.deltaTime;
            //Prediction jump
            if(Input.GetKeyUp(KeyCode.Space) && interval_jump_time > interval_jump_cd * jump_prediction)
                registered_input_jump = true;
        }
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(!is_dead)
        {
            if (IsMoving() && !little_bubbles.isPlaying)
                little_bubbles.Play();
            else if(!little_bubbles.isStopped)
                little_bubbles.Stop();
            Chargejump();
            Move();
        }
        else
        {
            //target.enabled = false;
            if (bubbles.isPlaying)
                bubbles.Stop();
            if (little_bubbles.isPlaying)
                little_bubbles.Stop();
            Deceleration();
            light.SetActive(false);
        }
            
	}

    private void Chargejump()
    {
        if (Input.GetKey(KeyCode.Space) && speed < max_speed)
        {
            loading_time += Time.deltaTime;
            if (loading_time > loading_cd)
            {
                speed = max_speed;
                if(!bubbles.isPlaying)
                    bubbles.Play();
                //if (speed > max_speed)
                //    speed = max_speed;
            }
            
        }
        else 
        {
            if(speed != max_speed && !bubbles.isStopped)
            {
                bubbles.Stop();
                
            }
            loading_time = 0;
            
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if(Input.GetKey(KeyCode.LeftControl))
                current_euler_angle -= rota_speed/ 6.5f;
            else
                current_euler_angle -= rota_speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.LeftControl))
                current_euler_angle += rota_speed / 6.5f;
            else
                current_euler_angle += rota_speed;

        }
        t.eulerAngles = new Vector3(0,0,current_euler_angle);
        if ((Input.GetKeyUp(KeyCode.Space) || registered_input_jump) && interval_jump_time <= 0)
        {
            /*count_deceler = 1;
            if (speed < init_speed)
            {
                speed += acceleration;
                if (speed > init_speed)
                    speed = init_speed;
            }
            //Vector2 v = body.GetRelativePointVelocity(t.position);
            //body.velocity = v * speed;*/
            interval_jump_time = interval_jump_cd;
            count_deceler = 1;
            registered_input_jump = false;
            if (body.velocity.y < threshold_speed && body.velocity.y > -threshold_speed
                && body.velocity.x < threshold_speed && body.velocity.x > -threshold_speed)
            {
                Freezing(false);
                body.AddRelativeForce(new Vector2(0, speed), ForceMode2D.Force);
            }
            speed = init_speed;

        }
        else
        {
            Deceleration();
        }
        //print(speed);
    }

    private void Freezing(bool b)
    {
        if (b)
            body.constraints = RigidbodyConstraints2D.FreezePosition;
        else
            body.constraints = RigidbodyConstraints2D.None;
    }

    private void Deceleration()
    {
        if (count_deceler > 0)
        {
            count_deceler -= deceleration;
            if (count_deceler < 0)
                count_deceler = 0;
        }

        body.velocity *= count_deceler;
        if (count_deceler == 0)
            Freezing(true);
        //print(body.velocity + " et " + count_deceler);
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //body.velocity = new Vector3(0, 0, 0);
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    //--------------------------------------------------------------------------------------------------------------------
    public bool IsMoving()
    {
        if (Mathf.Abs(body.velocity.x) > 0.5f || Mathf.Abs(body.velocity.y) > 0.5f)
            return true;
        return false;
    }
    public Vector3 GetPosition()
    {
        return t.position;
    }

    public float IsLoading()//<= 0 = pas en train de load
    {
        return speed - init_speed;
    }

    public void Grow(float g)
    {
        Vector3 v = t.localScale;
        v.x += g;
        v.y += g;
        v.z += g;
        t.localScale = v;
    }

    public bool IsDead
    {
        get { return is_dead; }
        set { is_dead = value; }
    }

    public bool LightModActivated
    {
        get { return light_mod_activated; }
        set { light_mod_activated = value;
            if(value)
            {
                light.SetActive(true);
                anime.ActivateLight();
            }
        }
    }
    public void SetVelocity(Vector3 v)
    {
        body.velocity = v;
    }
    public void SetCircleAlpha(float f)
    {
        circle_rend.color = new Color(1, 1, 1, f);
    }
}
