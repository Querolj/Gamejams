using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MeteorController : MonoBehaviour {
    public GameObject meteor;
    public GameObject smoke;
    public float pop_time_interval;
    public float turning_speed;
    public float interval_min = 0.1f;
    public float interval_droprate = 0.05f;

    private float pop_time;
    private List<Meteor> all_meteors_body;
    private Vector2[] interval;
    private int counter = 0;
    private float time_mutl = 2;
    void Start () {
        pop_time = pop_time_interval;
        all_meteors_body = new List<Meteor>();
        interval = new Vector2[2];
        interval[0] = GameObject.Find(this.name + "/It1").GetComponent<Transform>().position;
        interval[1] = GameObject.Find(this.name + "/It2").GetComponent<Transform>().position;
    }
	
	void Update () {
		if(pop_time > 0)
        {
            pop_time -= Time.deltaTime;
            if(pop_time <= 0)
            {
                PopMeteor();
                pop_time = pop_time_interval;
            }
        }

        if(all_meteors_body.Count > 0)
        {
            MeteorControl();
        }

        //Testing
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        DropRate();
	}

    private void DropRate()
    {
        //Mult$
        if(time_mutl > 0)
        {
            time_mutl -= Time.deltaTime;
            if(time_mutl <= 0)
            {
                if (pop_time_interval >= interval_min)
                {
                    pop_time_interval -= interval_droprate;
                }
                time_mutl = 1;
            }
        }
        
    }

    void PopMeteor()
    {
        Vector3 v = new Vector3(0, interval[0].y, 0);
        float r = Random.Range(0f, 1f);
        v.x = interval[0].x + (interval[1].x - interval[0].x) * r;

        GameObject o = Instantiate(meteor, v, Quaternion.identity);
        o.name = o.name + counter.ToString();
        Meteor m = o.GetComponent<Meteor>();
        counter++;
        //Lock l = Instantiate(smoke, v, Quaternion.identity).GetComponent<Lock>();
        //l.t_to_follow = m.GetTransform();
        all_meteors_body.Add(m);
        
        
    }

    void MeteorControl()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            foreach (Meteor m in all_meteors_body)
            {
                m.SetDirection(-120, -turning_speed);
            }
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            foreach (Meteor m in all_meteors_body)
            {
                m.SetDirection(-60, turning_speed);
            }
        }
        else
        {
            foreach (Meteor m in all_meteors_body)
            {
                m.SetDirection(-90, 0);
            }
        }
        
    }

    //------------------------------------------------------------------------------------------------------------------------
    public void RemoveMeteor(Meteor m)
    {
        all_meteors_body.Remove(m);
    }
}