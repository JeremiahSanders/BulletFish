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
        ChangeState(new GameStarting());
        ChangeState(new GameRunning());
    }

    private void ChangeState(GameState newState)
    {
        if (CurrentState != null) {
            CurrentState.TransitionOut(this);
        }
        CurrentState = newState;
        newState.TransitionIn(this);
    }

    private GameState CurrentState { get; set; }

    // Update is called once per frame
    private void Update()
    {
        CurrentState.Act(this);
    }

    private abstract class GameState {
        public virtual void Act(GameMachine machine) {}
        public virtual void TransitionIn(GameMachine machine) {}
        public virtual void TransitionOut(GameMachine machine) {}
    }

    private class GameStarting:GameState {
        private static string[] ClipNames = new[] {
                                                      IntroClip1,
                                                      IntroClip2
                                                  };
        private const string IntroClip1 = "1a. And they're off!";
        private const string IntroClip2 = "1b. Lets race boys and gills";
        public override void TransitionIn(GameMachine machine)
        {
            if (machine.AudioSource == null)return;
            int clipIndex = Random.Range(0, (ClipNames.Length - 1));
            string clipName = ClipNames[clipIndex];
            Debug.Log("Playing "+clipName);
            var clip = new AudioClip {name = clipName};
            machine.AudioSource.PlayVoiceoverClip(clip);
        }
    }

    private class GameRunning : GameState {
        private static string[] RandomClips = new[] {
                                                        "back to school"
                                                    };
        public override void Act(GameMachine machine)
        {
            if (machine.AudioSource == null) return;
            var shouldPlaySound = Random.value > 0.97;
            if (!shouldPlaySound)return;
            int clipIndex = Random.Range(0, (RandomClips.Length - 1));
            string clipName = RandomClips[clipIndex];
            var clip = new AudioClip { name = clipName };
            machine.AudioSource.PlayVoiceoverClip(clip);
        }
    }
}