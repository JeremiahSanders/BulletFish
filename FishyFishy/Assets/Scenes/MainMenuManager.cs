using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    public GameObject AudioPlayer;
    // Use this for initialization
    private void Start()
    {
        if (AudioPlayer == null) return;
        var manager = AudioPlayer.GetComponent<AudioMachine>();
        if (manager == null) return;
        manager.PlayMainMenuClips();
    }

    // Update is called once per frame
    private void Update() {}
}