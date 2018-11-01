using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvniSpawner : MonoBehaviour {
    public GameObject ovni;
    public float interval_cd = 12f;
    public int max_ovni = 3;
    public Transform[] spawn_points;
    public float droprate = 0.5f;
    public float interval_min = 2f;

    private float interval_time;
    private int count_ovni = 0;
    private int general_count = 0;
    private AudioSource source;
    private float time_droprate;
    void Start () {
        time_droprate = 7f;
        interval_time = interval_cd;
        source = this.GetComponent<AudioSource>();
    }
	
	void Update () {
        if(count_ovni > 0 && !source.isPlaying)
        {
            source.Play();
        }
        else if(count_ovni == 0 &&source.isPlaying)
        {
            source.Stop();
        }
		if(interval_time > 0 && count_ovni < max_ovni)
        {
            interval_time -= Time.deltaTime;
            if (interval_time <= 0)
            {
                int r = Random.Range(0, spawn_points.Length);
                GameObject o = GameObject.Instantiate(ovni, spawn_points[r].position, Quaternion.identity);
                o.name = o.name + " " + general_count.ToString();
                interval_time = interval_cd;
                count_ovni++;
                general_count++;
            }
        }
        Mult();
        
	}

    private void Mult()
    {
        if (time_droprate > 0 && interval_cd > interval_min)
        {
            time_droprate -= Time.deltaTime;
            if(time_droprate <= 0)
            {
                interval_cd -= droprate;
                time_droprate = 5f; 
            }
        }
        else
        {
            interval_cd = interval_min;
        }
    }

    public void KillOne()
    {
        count_ovni--;
    }
}
