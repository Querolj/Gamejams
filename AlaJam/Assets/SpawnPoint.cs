using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    public SpawnPoint next_point;

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public SpawnPoint GetPoint()
    {
        return next_point;
    }
}
