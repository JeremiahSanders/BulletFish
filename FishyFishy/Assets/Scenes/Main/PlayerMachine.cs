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

    private const float MaxIntensity = 33.3f;
    private const float MaxLungCapacity = 57.2f;

    public const string Player1Button = "Player1";
    public const string Player2Button = "Player2";
    public const string Player3Button = "Player3";
    public const string Player4Button = "Player4";
    private static readonly Dictionary<PlayerStates, PlayerState> StateLookup;
    public Text BreathDisplay;
    public PlayerIdentifier Control = PlayerIdentifier.Player1;

    private PlayerState CurrentPlayerState = StateLookup[PlayerStates.AtRest];
    private float CurrentRawBreathPressure;
    public Text LungDisplay;

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

    private PlayerStates CurrentState
    {
        get { return CurrentPlayerState.State; }
    }

    public bool IsBlowing
    {
        get { return CurrentState == PlayerStates.Blowing && CurrentLungCapacity > 5f; }
    }

    public float LungCapacityPercentage
    {
        get { return CurrentLungCapacity/MaxLungCapacity; }
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
        UpdateHud();
    }


    private void UpdateHud()
    {
        if (BreathDisplay != null) {
            BreathDisplay.text = String.Format("{0:P0}", CurrentBreathPressure);
        }
        if (LungDisplay != null) {
            LungDisplay.text = String.Format("{0:P0}", LungCapacityPercentage);
        }
    }

    private class AtRestState : PlayerState {
        private const float percentageReducedWhileResting = 21.5f;
        private const float RateOfBreathReplenishment = 26.4f;

        public override PlayerStates State
        {
            get { return PlayerStates.AtRest; }
        }

        public override void Act(PlayerMachine player)
        {
            player.CurrentRawBreathPressure = Mathf.Clamp(
                player.CurrentRawBreathPressure - (percentageReducedWhileResting*Time.deltaTime), 0,
                MaxIntensity);
            player.CurrentLungCapacity =
                Mathf.Clamp(player.CurrentLungCapacity + (RateOfBreathReplenishment*Time.deltaTime), 0,
                    MaxLungCapacity);
        }
    }

    private class BlowingState : PlayerState {
        private const float additionalBlowValue = 93.9f;
        private const float blowVolumeUse = 110f;

        public override PlayerStates State
        {
            get { return PlayerStates.Blowing; }
        }

        public override void Act(PlayerMachine player)
        {
            // they're holding down the button ...
            player.CurrentLungCapacity =
                Mathf.Clamp(player.CurrentLungCapacity - (blowVolumeUse*player.CurrentBreathPressure*Time.deltaTime), 0,
                    MaxLungCapacity);
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