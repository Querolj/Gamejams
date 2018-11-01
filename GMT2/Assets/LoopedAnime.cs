using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopedAnime : MonoBehaviour {
    public Sprite[] sprites;
    public int fps = 10;
    public bool on_off = false;

    private SpriteRenderer spriteRenderer;
    
	void Start () {
        spriteRenderer = this.GetComponent<SpriteRenderer>();

    }
	
	void Update () {
        if(on_off)
        {
            int index = (int)(Time.timeSinceLevelLoad * fps);
            index = index % sprites.Length;
            spriteRenderer.sprite = sprites[index];
        }
        else
        {
            spriteRenderer.sprite = null;
        }
    }

    public void Play()
    {
        on_off = true;
    }
    public void Stop()
    {
        on_off = false;
    }
}
