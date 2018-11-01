using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour {
    public int building_num = 0;

    private List<Building> buildings;//target possible pour les ovnis
    //Rules
    private bool game_over = false;
    private Text score_result;
    private Text pop_display;
    private GameObject go;
    private Score current_score;
    private string init_building_num;
    void Start () {
        if(buildings == null)
            buildings = new List<Building>();
        go = GameObject.Find("GameOver");
        score_result = GameObject.Find("GameOver/ScoreResult").GetComponent<Text>();
        pop_display = GameObject.Find("Score/Population").GetComponent<Text>();
        current_score = GameObject.Find("Score/ScoreDisplay").GetComponent<Score>();
        go.SetActive(false);
        init_building_num = building_num.ToString();
        pop_display.text = building_num.ToString() + "/" + building_num.ToString();
    }
	
	void Update () {
        //print(buildings.Count);
        if (building_num == 0 && !game_over)
        {
            //end game
            game_over = true;
            print("end game");
            score_result.text = current_score.GetScore();
            current_score.StopScore();
            go.SetActive(true);
        }
        else if(buildings == null || buildings.Count < 1)
        {
            /*
            Building[] child_buildings = GameObject.Find("Buildings").GetComponentsInChildren<Building>();
            if(child_buildings!= null)
            {
                foreach (Building b in child_buildings)
                {
                    if (b != null)
                        buildings.Add(b);
                }
            }*/
        }
        
    }

    public void RemoveBuilding(Building b)
    {
        buildings.Remove(b);
    }

    public Building PickUpBuilding()
    {
        int choosen_building = Random.Range(0, buildings.Count);
        int c = 0;
        foreach(Building b in buildings)
        {
            if (c == choosen_building)
            {
                RemoveBuilding(b);
                return b;
            }
            c++;
        }
        return null;
    }

    public void AddBuilding(Building b)
    {
        if(buildings == null)
            buildings = new List<Building>();
        if (b != null)
            buildings.Add(b);
    }

    public void DestroyBuilding()
    {
        if(building_num > 0)
        {
            building_num--;
            pop_display.text = building_num.ToString() + "/" + init_building_num;
        }
    }
}
