using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFish : MonoBehaviour {

    private Vector3[] waypoints;
	void Start () {
        int waypoints_number = this.gameObject.transform.childCount;
        waypoints = new Vector3[waypoints_number];
        for(int i=0;i< waypoints_number;i++)
        {
            string path = this.name + "/W1 (" + i.ToString() + ")";
            waypoints[i] = GameObject.Find(path).GetComponent<Transform>().position;
        }
    }
	
	void Update () {
		
	}

    //------------------------------------------------------------------------------------------------------------------------
    public Vector3[] GetWaypoints()
    {
        if (waypoints == null)
        {
            int waypoints_number = this.gameObject.transform.childCount;
            waypoints = new Vector3[waypoints_number];
            for (int i = 0; i < waypoints_number; i++)
            {
                string path = this.name + "/W1 (" + i.ToString() + ")";
                waypoints[i] = GameObject.Find(path).GetComponent<Transform>().position;
            }
        }
        return waypoints;
    }
}
