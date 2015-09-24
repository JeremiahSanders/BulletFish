using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {
    public GameObject CutsceneCamera;
    public GameObject MainCamera;
    public GameObject HudWrapper;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void DoIntroSweep()
    {
        CutsceneCamera.SetActive(true);
        MainCamera.SetActive(false);


    }

    enum CameraStates {
        IntroSweep,
        Game
    }
}
