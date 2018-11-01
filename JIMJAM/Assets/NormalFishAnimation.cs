using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFishAnimation : MonoBehaviour {
    public Sprite initSprite;
    public Sprite[] spritesSwimming;

    public float fps_swimming = 15;

    private SpriteRenderer rend;
    private NormalFish fish;
    void Start () {
        rend = this.GetComponent<SpriteRenderer>();
        fish = this.GetComponent<NormalFish>();
    }
	
	// Update is called once per frame
	void Update () {
        if (fish.IsMoving())
        {
            int index_swimming = (int)(Time.timeSinceLevelLoad * fps_swimming);
            index_swimming = index_swimming % spritesSwimming.Length;
            rend.sprite = spritesSwimming[index_swimming];
        }
        else
            rend.sprite = initSprite;

    }
}
