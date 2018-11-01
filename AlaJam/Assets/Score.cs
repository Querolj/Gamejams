using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour {

    //private int score = 0;
    private float counter = 0;
    private Text display;
    public bool stop = false;
	void Start () {
        display = this.GetComponent<Text>();

    }
	
	void Update () {
        if(!stop)
        {
            counter += Time.deltaTime;
            display.text = GetScore();
        }
        
    }

    public string GetScore()
    {
        return counter.ToString("n1");
    }

    public void StopScore()
    {
        stop = true;
    }
}
