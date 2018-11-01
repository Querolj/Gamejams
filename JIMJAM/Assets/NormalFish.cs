using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFish : MonoBehaviour {
    public CircleFish circle_fish;
    public float speed = 2f;
    public int current_waypoint = 0;
    public bool load_player = true;

    private Rigidbody2D body;
    private Vector3[] waypoints;
    private Transform fish_t;
    private Rigidbody2D fish_body;
    private SpriteRenderer rend;
    private bool is_dead = false;
    private Vector3 priority_focus;
    private bool is_focused = false;
    private PlayerController player;
    private string last_waypoint = "";
    private string circle_name;
    private float initial_speed;
    void Start () {
        initial_speed = speed;
        waypoints = circle_fish.GetWaypoints();
        fish_body = this.GetComponent<Rigidbody2D>();
        fish_t = this.GetComponent<Transform>();
        if(load_player)
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        rend = this.GetComponent<SpriteRenderer>();
        fish_t.eulerAngles = new Vector3(0,0,-90);
        circle_name = circle_fish.gameObject.name;
    }
	
	void Update () {
	    if(!is_dead)
        {
            if(!is_focused)
            {
                LookWaypoint(waypoints[current_waypoint]);
                MoveToWaypoint(waypoints[current_waypoint]);
            }else
            {
                priority_focus = player.GetPosition();
                LookWaypoint(priority_focus);
                MoveToWaypoint(priority_focus);
            }
            
        }
	}

    private void LookWaypoint(Vector3 waypoint)
    {
        var dir = fish_t.position - waypoint;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        fish_t.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void MoveToWaypoint(Vector3 waypoint)
    {
        //e_body.MovePosition(player_t.position);
        float x_velo = fish_t.position.x - waypoint.x;
        float y_velo = fish_t.position.y - waypoint.y;
        float total = Mathf.Abs(x_velo) + Mathf.Abs(y_velo);
        x_velo = (x_velo / total) * speed;
        y_velo = (y_velo / total) * speed;
        fish_body.velocity = new Vector2(-x_velo, -y_velo);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "waypoint" && last_waypoint != collision.gameObject.name && circle_name == collision.transform.parent.gameObject.name && !is_focused)
        {
            last_waypoint = collision.gameObject.name;
            current_waypoint = (current_waypoint + 1) % waypoints.Length;
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------
    public bool IsMoving()
    {
        if (Mathf.Abs(fish_body.velocity.x) > 0.2f || Mathf.Abs(fish_body.velocity.y) > 0.2f)
            return true;
        return false;
    }
    public void NewFocus(Vector3 v)
    {
        speed = initial_speed/2;
        is_focused = true;
        priority_focus = v;
    }
    public void NoFocus()
    {
        last_waypoint = "";
        is_focused = false;
        speed = initial_speed;
    }
}
