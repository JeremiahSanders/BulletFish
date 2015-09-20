using System.Collections.Generic;
using UnityEngine;

public class GameMachine : MonoBehaviour {
    static GameMachine()
    {
        PlayerCount = 4;
        Winners = new List<PlayerMachine.PlayerIdentifier>();
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
    private void Start() {}

    // Update is called once per frame
    private void Update() {}
}