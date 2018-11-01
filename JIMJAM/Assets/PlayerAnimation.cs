using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    public Sprite initSprite;
    public Sprite initSpriteLight;
    public Sprite[] spritesSwimming;
    public Sprite[] spritesSwimmingLight;
    public Sprite[] spritesDeath;

    public float fps_swimming;
    public float fps_loading;
    public float time_death_cd = 0.1f;
    [Space]
    public AudioClip[] splashs;
    public AudioClip water_dash;
    public float int_splash_sound = 0.15f;

    private PlayerController player;
    private SpriteRenderer rend;
    private float time_death;
    private int count_death = 0;
    private AudioSource source;
    private int count_splash = 0;
    private float int_splash_time;
    private bool dashed = false;
    private bool play_on_dash = true;
    void Start () {
        time_death = 0;
        int_splash_time = int_splash_sound;
        player = this.GetComponent<PlayerController>();
        rend = this.GetComponent<SpriteRenderer>();
        source = this.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        

        if(int_splash_time > 0)
        {
            int_splash_time -= Time.deltaTime;
            
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            SimplePlayingSplash();
        }

        if(player.IsDead)
        {
            if(time_death > 0 )
            {
                time_death -= Time.deltaTime;
                
            }
            else if(count_death < spritesDeath.Length)
            {
                time_death = time_death_cd;
                rend.sprite = spritesDeath[count_death];
                count_death++;
            }
        }
        else if(player.IsLoading() > 1)
        {
            dashed = true;
            int index_swimming = (int)(Time.timeSinceLevelLoad * fps_loading);
            index_swimming = index_swimming % spritesSwimming.Length;
            
            rend.sprite = spritesSwimming[index_swimming];
            PlayingSplash(index_swimming);
        }
        else if (player.IsMoving() && !player.IsDead)
        {
            
            int index_swimming = (int)(Time.timeSinceLevelLoad * fps_swimming);
            index_swimming = index_swimming % spritesSwimming.Length;
            rend.sprite = spritesSwimming[index_swimming];
            //if(!source.isPlaying)
            if(!dashed)
                PlayingSplash(index_swimming);
            else
            {
                if(play_on_dash)
                {
                    play_on_dash = false;
                    dashed = false;
                    source.PlayOneShot(water_dash);
                }
                if (!source.isPlaying)
                    dashed = false;
            }
        }
        else
        {
            rend.sprite = initSprite;
        }

	}

    private void PlayingSplash(int index_swimming)
    {
        if (index_swimming % spritesSwimming.Length == 0 && int_splash_time <= 0)
        {
            if (count_splash >= splashs.Length)
                count_splash = 0;
            source.PlayOneShot(splashs[count_splash]);
            count_splash++;
            int_splash_time = int_splash_sound;
            play_on_dash = true;
        }
    }
    private void SimplePlayingSplash()
    {
        if(int_splash_time <= 0)
        {
            if (count_splash >= splashs.Length)
                count_splash = 0;
            source.PlayOneShot(splashs[count_splash]);
            count_splash++;
            int_splash_time = int_splash_sound;
            play_on_dash = true;
        }
        
    }
    //--------------------------------------------------------------------------------------------------------------------------------
    public void ActivateLight()
    {
        spritesSwimming = spritesSwimmingLight;
        initSprite = initSpriteLight;
    }
}
