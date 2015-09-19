using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMachine : MonoBehaviour {
    public enum PlayerIdentifier {
        Player1 = 0,
        Player2 = 1,
        Player3 = 2,
        Player4 = 3
    }

    public enum PlayerStates {
        AtRest = 0,
        Blowing = 1
    }

    private const float IntensityDecreaseRate = 3.2f;
    private const float IntensityIncreaseRate = 8.2f;
    private const float MaxIntensity = 50;
    private const float MaxLungCapacity = 150f;

    public const string Player1Button = "Player1";
    public const string Player2Button = "Player2";
    public const string Player3Button = "Player3";
    public const string Player4Button = "Player4";
    private const float RateOfBreathReplenishment = 75f;
    private static readonly Dictionary<PlayerStates, PlayerState> StateLookup;

    private  PlayerStates CurrentState {get { return CurrentPlayerState.State; } }
    public Text BreathDisplay;
    public PlayerIdentifier Control = PlayerIdentifier.Player1;

    private PlayerState CurrentPlayerState = StateLookup[PlayerStates.AtRest];
    private float CurrentRawBreathPressure;

    public GameObject FishModel;

    static PlayerMachine()
    {
        StateLookup = new Dictionary<PlayerStates, PlayerState> {
                                                                    {PlayerStates.Blowing, new BlowingState()},
                                                                    {PlayerStates.AtRest, new AtRestState()}
                                                                };
    }

    public float CurrentBreathPressure
    {
        get { return CurrentRawBreathPressure <= 0 ? 0 : CurrentRawBreathPressure/MaxIntensity; }
    }

    public float CurrentLungCapacity { get; private set; }

    public bool IsBlowing
    {
        get { return CurrentState == PlayerStates.Blowing; }
    }

    private static string GetButtonCode(PlayerIdentifier player)
    {
        switch (player) {
            case PlayerIdentifier.Player1:
                return Player1Button;
            case PlayerIdentifier.Player2:
                return Player2Button;
            case PlayerIdentifier.Player3:
                return Player3Button;
            case PlayerIdentifier.Player4:
                return Player4Button;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    // Use this for initialization
    private void Start()
    {
        CurrentLungCapacity = MaxLungCapacity;
    }

    // Update is called once per frame
    private void Update()
    {
        var buttonVal = Input.GetButton(GetButtonCode(Control));
        var desiredState = buttonVal ? PlayerStates.Blowing : PlayerStates.AtRest;
        CurrentPlayerState.ChangePlayerState(this, desiredState);
        CurrentPlayerState.Act(this);
        //if (buttonVal) {
        //    if (CurrentState == PlayerStates.AtRest) {
        //        CurrentState = PlayerStates.Blowing;
        //    }
        //}
        //else {
        //    CurrentState = PlayerStates.AtRest;
        //}
        //UpdateBreathPressure();
        //UpdateLungCapacity();
        UpdateHud();
    }

    private void UpdateBreathPressure()
    {
        if (CurrentState == PlayerStates.Blowing) {
            CurrentRawBreathPressure += IntensityIncreaseRate*Time.deltaTime;
        }
        else {
            CurrentRawBreathPressure -= IntensityDecreaseRate*Time.deltaTime;
        }
        CurrentRawBreathPressure = Mathf.Clamp(CurrentRawBreathPressure, 0, MaxIntensity);
    }

    private void UpdateHud()
    {
        if (BreathDisplay != null) {
            BreathDisplay.text = String.Format("{2:P}", CurrentLungCapacity, MaxLungCapacity,
                CurrentBreathPressure);
        }
    }

    private void UpdateLungCapacity()
    {
        if (CurrentState == PlayerStates.AtRest) {
            CurrentLungCapacity += RateOfBreathReplenishment*Time.deltaTime;
        }
        else if (CurrentState == PlayerStates.Blowing) {
            CurrentLungCapacity -= CurrentRawBreathPressure;
        }
        CurrentLungCapacity = Mathf.Clamp(CurrentLungCapacity, 0, MaxLungCapacity);
    }

    private class AtRestState : PlayerState {
        private const float percentageReducedWhileResting = 2.5f;

        public override PlayerStates State
        {
            get { return PlayerStates.AtRest; }
        }

        public override void Act(PlayerMachine player)
        {
            player.CurrentRawBreathPressure = Mathf.Clamp(
                player.CurrentRawBreathPressure - (percentageReducedWhileResting*Time.deltaTime), 0,
                MaxIntensity);
        }
    }

    private class BlowingState : PlayerState {
        private const float additionalBlowValue = 24.8f;

        public override PlayerStates State
        {
            get { return PlayerStates.Blowing; }
        }

        public override void Act(PlayerMachine player)
        {
            // they're holding down the button ...
            StateLookup[PlayerStates.AtRest].Act(player);
        }

        protected override void TransitionIntoState(PlayerMachine player)
        {
            player.CurrentRawBreathPressure =
                Mathf.Clamp(player.CurrentRawBreathPressure + (additionalBlowValue*Time.deltaTime), 0,
                    MaxIntensity);
        }
    }

    private abstract class PlayerState {
        public abstract PlayerStates State { get; }

        public virtual void Act(PlayerMachine player) {}

        public void ChangePlayerState(PlayerMachine player, PlayerStates desiredState)
        {
            if (desiredState == State) return;
            TransitionOutOfState(player);
            TransitionIntoState(player);
            player.CurrentPlayerState = StateLookup[desiredState];
        }

        protected virtual void TransitionIntoState(PlayerMachine player) {}
        protected virtual void TransitionOutOfState(PlayerMachine player) {}
    }
}