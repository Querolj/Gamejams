using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour {

	[Tooltip("_sceneToLoadOnPlay is the name of the scene that will be loaded when users click play")]
	public string _sceneToLoadOnPlay = "Level";
	[Tooltip("_webpageURL defines the URL that will be opened when users click on your branding icon")]
	public string _webpageURL = "http://www.alpaca.studio";
	[Tooltip("_soundButtons define the SoundOn[0] and SoundOff[1] Button objects.")]
	public Button[] _soundButtons;
	[Tooltip("_audioClip defines the audio to be played on button click.")]
	public AudioClip _audioClip;
	[Tooltip("_audioSource defines the Audio Source component in this scene.")]
	public AudioSource _audioSource;
	
	//The private variable 'scene' defined below is used for example/development purposes.
	//It is used in correlation with the Escape_Menu script to return to last scene on key press.
	UnityEngine.SceneManagement.Scene scene;

    public int max_level = 6;
    public Text level_display;

    private int level_selected = 1;

    void Awake () {
		if(!PlayerPrefs.HasKey("_Mute")){
			PlayerPrefs.SetInt("_Mute", 0);
		}
        scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
		PlayerPrefs.SetString("_LastScene", scene.name.ToString());
        //Debug.Log(scene.name);
        UpdateDisplay();
        Unmute();
    }
	
	public void OpenWebpage () {
		_audioSource.PlayOneShot(_audioClip);
		Application.OpenURL(_webpageURL);
	}
	
	public void PlayGame () {
        /*_audioSource.PlayOneShot(_audioClip);
		PlayerPrefs.SetString("_LastScene", scene.name);
		UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneToLoadOnPlay);*/
        SceneManager.LoadScene("Level"+level_selected.ToString());
	}
	
	public void Mute () {
		_audioSource.PlayOneShot(_audioClip);
		_soundButtons[0].interactable = true;
		_soundButtons[1].interactable = false;
		PlayerPrefs.SetInt("_Mute", 1);
	}
	
	public void Unmute () {
		_audioSource.PlayOneShot(_audioClip);
		_soundButtons[0].interactable = false;
		_soundButtons[1].interactable = true;
		PlayerPrefs.SetInt("_Mute", 0);
	}
	
	public void QuitGame () {
		_audioSource.PlayOneShot(_audioClip);
		#if !UNITY_EDITOR
			Application.Quit();
		#endif
		
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

    //Mod
    public void RightArrow()
    {
        if (level_selected < max_level)
            level_selected++;
        UpdateDisplay();
    }

    public void LeftArrow()
    {
        if (level_selected > 1)
            level_selected--;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        level_display.text = level_selected.ToString()+"/"+max_level.ToString();
    }
}

