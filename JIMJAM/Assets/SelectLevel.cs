using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectLevel : MonoBehaviour {
    public int max_level = 6;

    private Text level_display;
    private int level_selected = 1;
	void Start () {
        level_display = this.GetComponentInChildren<Text>();

    }
	
	void Update () {
		
	}

    private void RightArrow()
    {
        if (level_selected < max_level)
            level_selected++;
    }

    private void LeftArrow()
    {
        if (level_selected > 0)
            level_selected--;
    }

    private void UpdateDisplay(int n)
    {
        level_display.text = level_selected.ToString();
    }
}
