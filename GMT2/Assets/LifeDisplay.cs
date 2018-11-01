using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeDisplay : MonoBehaviour {

    public int life = 3;

    private Image[] hearts;
	void Start () {
        hearts = new Image[life];
		for(int i=0;i<life;i++)
        {
            string n = (i+1).ToString();
            hearts[i] = GameObject.Find("Life/H" + n).GetComponent<Image>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hurted()
    {
        if(life > 0)
            life--;
        hearts[life].sprite = null;
        hearts[life].color = new Color(255, 255, 255, 0);
    }
}
