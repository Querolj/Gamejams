using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingReference : MonoBehaviour {
    private Building building_ref;
	
    void Start()
    {
        building_ref = this.GetComponentInParent<Building>();
    }

    public void Damaged()
    {
        if (building_ref != null)
            building_ref.Damaged();
        else
            print("building_ref null");
    }
}
