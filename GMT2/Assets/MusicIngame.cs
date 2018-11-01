using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicIngame : MonoBehaviour {

    public AudioClip music01;
    public AudioClip music02;
    public AudioClip music03;
    [Range(1,3)]
    public int music_selected = 1;
    private AudioSource source;
    void Start () {
        source = this.GetComponent<AudioSource>();
        if (music_selected == 1)
            source.clip = music01;
        else if (music_selected == 2)
            source.clip = music02;
        else
            source.clip = music03;
        source.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
