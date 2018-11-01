using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    public Sprite[] damaged_sprites;
    public Sprite destroyed_sprite;
    public int life = 2;
    public Collider2D second_collider;

    private int init_life;
    private int count_damaged_sprite;
    private SpriteRenderer rend;
    private Collider2D base_collider;
    private BuildingManager manager;
	void Start () {
        init_life = life;
        rend = this.GetComponent<SpriteRenderer>();
        base_collider = this.GetComponent<Collider2D>();
        manager = this.GetComponentInParent<BuildingManager>();
        manager.AddBuilding(this);
        if(second_collider != null)
        {
            second_collider.enabled = false;
        }
    }
	
	void Update () {
		
	}

    public void Damaged()
    {
        if(life > 0)
        {
            rend.sprite = damaged_sprites[count_damaged_sprite];
            count_damaged_sprite++;
            life--;
            if (life == 0 && second_collider != null)
            {
                base_collider.enabled = false;
                second_collider.enabled = true;
            }
        }
        else{
            manager.DestroyBuilding();
            rend.sprite = destroyed_sprite;
            if (second_collider != null && second_collider.enabled)
                second_collider.enabled = false;
            else
                base_collider.enabled = false;

        }
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public bool IsDestroyed()
    {
        if(second_collider != null && second_collider.enabled)
            return !second_collider.enabled;
        return !base_collider.enabled;
    }

}
