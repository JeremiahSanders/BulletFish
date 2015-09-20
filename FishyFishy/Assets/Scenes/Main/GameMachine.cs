using System;
using System.Linq;
using UnityEngine;

public class GameMachine : MonoBehaviour {
    private readonly PlayerMachine.PlayerIdentifier?[] _winners = new PlayerMachine.PlayerIdentifier?[4];

    private void EveryoneFinished()
    {
        Debug.Log("Game Over -- Every Fish Completed!");
        Debug.Log(String.Format("1st Place - {0}",_winners[0]));
        Debug.Log(String.Format("2nd Place - {0}", _winners[1]));
        Debug.Log(String.Format("3rd Place - {0}", _winners[2]));
        Debug.Log(String.Format("4th Place - {0}", _winners[3]));
        Application.LoadLevel("Score Scene_003");
    }

    public void ReachedFinishLine(PlayerMachine.PlayerIdentifier player)
    {
        if (_winners.Contains(player)) return;
        if (_winners[0] == null) {
            _winners[0] = player;
            return;
        }
        if (_winners[1] == null) {
            _winners[1] = player;
            return;
        }
        if (_winners[2] == null) {
            _winners[2] = player;
            return;
        }
        if (_winners[3] == null) _winners[3] = player;
        // all winners are assigned, so game is over
        EveryoneFinished();
    }

    // Use this for initialization
    private void Start() {}

    // Update is called once per frame
    private void Update() {}
}