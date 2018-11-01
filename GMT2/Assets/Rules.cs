using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rules : MonoBehaviour {

    public string next_level = "";
    public float end_cd = 0.7f;

    private int ennemi_number = 0;
    private GameObject retry_UI;
    private GameObject win_UI;
    private bool lost = false;
    private bool win = false;
    void Start () {
        retry_UI = GameObject.Find("RetryUI");
        win_UI = GameObject.Find("WinUI");
        retry_UI.SetActive(false);
        win_UI.SetActive(false);

        //Gestino des Layers
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnnemiCol"), LayerMask.NameToLayer("Default"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnnemiCol"), LayerMask.NameToLayer("Player"));

    }
	
	void Update () {
        if(win)
        {
            end_cd -= Time.deltaTime;
            if (end_cd <= 0)
                Win();
        }

        if (ennemi_number == 0 && !lost)
        {
            win = true;
            //Win();
        }
            
        if (Input.GetKeyUp(KeyCode.R))
            Retry();
        if (Input.GetKeyUp(KeyCode.M))
            MainMenu();
        if (Input.GetKeyUp(KeyCode.N) && win)
            NextLevel();
    }

    private void Win()
    {
        win = true;
        win_UI.SetActive(true);
    }

    //---------------------------------------------------------------------------------------------------------
    public bool HasWon()
    {
        return win;
    }
    public void AddEnnemi()
    {
        ennemi_number++;
    }
    public void RemoveEnnemi()
    {
        ennemi_number--;
    }

    public void ActivateRetryUI()
    {
        retry_UI.SetActive(true);
        lost = true;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        if(next_level != "")
            SceneManager.LoadScene(next_level);
    }
}
