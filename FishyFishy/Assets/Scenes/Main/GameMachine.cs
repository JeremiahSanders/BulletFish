using System.Collections.Generic;
using UnityEngine;

public class GameMachine : MonoBehaviour {
    public GameObject AudioManagerSource;

    static GameMachine()
    {
        PlayerCount = 4;
        Winners = new List<PlayerMachine.PlayerIdentifier>();
    }

    private AudioMachine AudioSource
    {
        get { return AudioManagerSource != null ? AudioManagerSource.GetComponent<AudioMachine>() : null; }
    }

    public static int PlayerCount { get; set; }
    public static List<PlayerMachine.PlayerIdentifier> Winners { get; private set; }

    private void EveryoneFinished()
    {
        Debug.Log("Game Over -- Every Fish Completed!");
        Application.LoadLevel("Score Scene_003");
    }

    public void ReachedFinishLine(PlayerMachine.PlayerIdentifier player)
    {
        if (Winners.Contains(player)) return;
        Winners.Add(player);
        if (Winners.Count == PlayerCount) {
            // all winners are assigned, so game is over
            EveryoneFinished();
        }
    }

    // Use this for initialization
    private void Start()
    {
        // reset winners each time we fire up the scene
        Winners.Clear();
    }

    // Update is called once per frame
    private void Update() {}

    private abstract class GameState {
        protected virtual void Act(GameMachine machine) {}
        protected virtual void TransitionIn(GameMachine machine) {}
        protected virtual void TransitionOut(GameMachine machine) {}
    }
}