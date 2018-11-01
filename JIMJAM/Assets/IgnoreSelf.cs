using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreSelf : MonoBehaviour {
    public GameObject[] objs_col;//fishs_col;

    void Start () {
        Collider2D[] selfs_col = this.GetComponentsInChildren<Collider2D>();
        List<Collider2D> fishs_col = new List<Collider2D>();
        foreach (GameObject obj in objs_col)
        {
            foreach(Collider2D fish_col in obj.GetComponentsInChildren<Collider2D>())
            {
                fishs_col.Add(fish_col);
            }
        }

        print(fishs_col.Capacity);
        if(selfs_col != null && fishs_col.Capacity > 0)
        {
            foreach(Collider2D fish_col in fishs_col)
            {
                foreach(Collider2D self_col in selfs_col)
                {
                    Physics2D.IgnoreCollision(fish_col, self_col);
                }
            }
        }
    }
	
}
