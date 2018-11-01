using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Rules : MonoBehaviour {
    public string next_level="";
    public GameObject level_competed;
    public GameObject game_over;

    private int food_num;
    private PlayerController player;
    private bool stop = false;
    void Start () {
        food_num = this.GetComponentsInChildren<Food>().Length;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        level_competed.SetActive(false);
        game_over.SetActive(false);
    }

    private void Update()
    {
        if (!stop)
        {
            if (food_num <= 0)
            {
                LevelCompleted();
            }
            if (player.IsDead)
                GameOver();
        }
        else if (Input.GetKeyDown(KeyCode.N))
            NextLevel();
        
    }

    private void LevelCompleted()
    {
        print("level complete");
        stop = true;
        level_competed.SetActive(true);
    }
    private void GameOver()
    {
        stop = true;
        game_over.SetActive(true);
    }
    //----------------------------------------------------------------------------------------------------------------
    public void LessFood()
    {
        food_num--;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(next_level);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
