using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovni : MonoBehaviour {
    public LineRenderer beam;
    public Transform sight_start;
    public Sprite[] beam_effect_sprites;
    public Sprite[] ovni_anime;
    public float beam_fps = 8;
    public bool beam_activated = false;
    public float time_unit_dmg = 1f;
    public float speed = 1.5f;
    public float beam_clignote = 0.1f;
    public Sprite[] sprites_explosion;
    public float fps_ovni = 12;
    //Death anime
    public float time_explosion_cd;

    private float time_explosion;
    private int count_explosion = 0;
    private SpriteRenderer rend;
    private bool is_dead = false;
    //Follow waypoints if no building locked
    private Vector3[] waypoints; //Correspond aux points de l'intervalle
    private Rigidbody2D body;
    //Beam stuff
    private Transform beam_effect_t;
    private SpriteRenderer beam_effect_rend;
    private float dmg_time = 0;
    //Lock a building
    private Building building_locked = null;
    private Vector3 current_target;
    private BuildingManager building_manager;
    private bool no_building = false;
    private GameObject destructor_rend;
    private float beam_clignote_time = 0;
    private bool act_clignote = false;
    //Spawn stuff
    private OvniSpawner spawner;
    //Sound
    private AudioSource source;

    void Start () {
        rend = this.GetComponent<SpriteRenderer>();
        time_explosion = time_explosion_cd;
        source = this.GetComponent<AudioSource>();

        spawner = GameObject.Find("OvniSpawner").GetComponent<OvniSpawner>();
        GameObject o = GameObject.Find(this.name + "/BeamEffect");
        destructor_rend = GameObject.Find(this.name + "/DestructorRenderer");
        beam_effect_rend = o.GetComponent<SpriteRenderer>();
        beam_effect_t = o.GetComponent<Transform>();
        building_manager = GameObject.Find("BuildingManager").GetComponentInParent<BuildingManager>();
        body = this.GetComponent<Rigidbody2D>();
        beam.enabled = false;
        PickUpBuilding();
    }
	
	void Update () {
        if(is_dead)
        {
            AnimeDeath();
            if (beam_activated)
                DesactivateBeam();
        }
        else
        {
            BeamBehaviour();
            if (!beam_activated)
            {
                MoveAI();
                MoveAnime();
            }
                
        }
        

    }

    private void MoveAnime()
    {
        int index = (int)(Time.timeSinceLevelLoad * fps_ovni);
        index = index % ovni_anime.Length;
        rend.sprite = ovni_anime[index];

    }

    private void AnimeDeath()
    {
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        if (time_explosion > 0)
        {
            time_explosion -= Time.deltaTime;
            if (time_explosion <= 0)
            {
                if (count_explosion >= sprites_explosion.Length)
                {
                    GameObject.Destroy(this.gameObject);
                }
                else
                {
                    rend.sprite = sprites_explosion[count_explosion];
                    count_explosion++;
                    time_explosion = time_explosion_cd;
                }
            }
        }
    }

    private void BeamBehaviour()
    {
        if (beam_activated && beam_effect_t != null)
        {
            //Gestion du beam renderer
            //Activé ici pour éviter retard de frame
            if (destructor_rend != null && !destructor_rend.activeInHierarchy)
                destructor_rend.SetActive(true);
            if (beam_clignote_time > 0)
            {
                beam_clignote_time -= Time.deltaTime;
                if (beam_clignote_time <= 0)
                {
                    if (act_clignote)
                        beam.widthMultiplier = 0.3f;
                    else
                        beam.widthMultiplier = 0.5f;
                    act_clignote = !act_clignote;
                    beam_clignote_time = beam_clignote;
                }
            }
            if (!beam.enabled)
                beam.enabled = true;

            int index_beam = (int)(Time.timeSinceLevelLoad * beam_fps);
            Vector3 down = this.transform.position;
            down.y = down.y - 10f;
            RaycastHit2D ray = Physics2D.Linecast(this.transform.position, down, LayerMask.NameToLayer("Ovni"));
            beam_effect_t.position = ray.point;
            index_beam = index_beam % beam_effect_sprites.Length;
            beam_effect_rend.sprite = beam_effect_sprites[index_beam];
            beam.SetPosition(1, ray.point);
            beam.SetPosition(0, sight_start.position);

            //Dommage au building
            if (dmg_time > 0)
            {
                dmg_time -= Time.deltaTime;
                if (dmg_time <= 0)
                {
                    if (building_locked != null || !building_locked.IsDestroyed())
                    {
                        building_locked.Damaged();
                    }
                    else
                    {
                        PickUpBuilding();
                        return;
                    }
                    dmg_time = time_unit_dmg;
                    
                }
            }
            if (building_locked != null && building_locked.IsDestroyed())
            {
                building_manager.RemoveBuilding(building_locked);
                PickUpBuilding();
            }
        }
    }

    private void MoveAI()
    {
        PickUpBuilding();
        if(building_locked != null)
        {
            float distance = current_target.x - this.transform.position.x;
            if(distance < 0)
                body.velocity = new Vector3(-speed, body.velocity.y);
            else
                body.velocity = new Vector3(speed, body.velocity.y);
            if (Mathf.Abs(distance) <= 0.02f && !beam_activated)
                ActivateBeam();

        }
        
    }

    private void ActivateBeam()
    {
        
        beam_activated = true;
        body.velocity = new Vector2(0, 0);
        dmg_time = time_unit_dmg;
        beam_clignote_time = beam_clignote;
        source.Play();
    }

    private void DesactivateBeam()
    {
        beam_effect_rend.sprite = null;
        beam_activated = false;
        if (destructor_rend.activeInHierarchy)
            destructor_rend.SetActive(false);
        if (source.isPlaying)
            source.Stop();
    }

    private void PickUpBuilding()
    {
        if (beam_activated)
        {
            DesactivateBeam();
        }

        if (building_locked == null || building_locked.IsDestroyed())
        {
            if (building_manager == null)
            {
                building_manager = GameObject.Find("BuildingManager").GetComponentInParent<BuildingManager>();
            }
            building_locked = building_manager.PickUpBuilding();
            if (building_locked == null)
                no_building = true;
            else
                current_target = building_locked.GetPosition();
        }
    }

    public void Kill()
    {
        is_dead = true;
        spawner.KillOne();
        if(building_locked != null)
        {
            building_manager.AddBuilding(building_locked);
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(beam_activated && collision.gameObject.tag == "building")
        {
            //building_locked = collision.gameObject.GetComponent<Building>();
            dmg_time = time_unit_dmg;
        }
    }*/
}
