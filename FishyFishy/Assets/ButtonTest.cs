using UnityEngine;
using System.Collections;

public class ButtonTest : MonoBehaviour {
    public GameObject TutorialPanel;
    public void TutorialButton()
    {
        Debug.Log("Tutorial button clicked");
        if(TutorialPanel !=null)TutorialPanel.SetActive(true);
    }

    public void HideTutorial()
    {
        if (TutorialPanel != null) TutorialPanel.SetActive(false);
    }

    public void PlayAgainButton()
    {
        Debug.Log("Restarting game");
        Application.LoadLevel("MainScene");
    }

    public void optionsButton()
    {
        Debug.Log("Options Button was pressed");
        Application.LoadLevel("Options Scene_001");
    }

    public void mainMenuButton()
    {
        Debug.Log("Heading to Bullet Fish Main Menu");
        Application.LoadLevel("Bullet Fish Main Menu UI_002");
    }

    public void creditsButton()
    {
        Debug.Log("Credits002 Button was pressed");
        Application.LoadLevel("Credit Scene_002");
    }

    public void playButton()
    {
        Debug.Log("Restarting game");
        Application.LoadLevel("MainScene");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
