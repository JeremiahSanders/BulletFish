using System;
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

    public const string Player1Button = "Player1";
    public const string Player2Button = "Player2";
    public const string Player3Button = "Player3";
    public const string Player4Button = "Player4";
    public Text BreathDisplay;
    public PlayerIdentifier Control = PlayerIdentifier.Player1;
    public float CurrentBreathPressure;
    public float CurrentLungCapacity = 150f;

    private PlayerStates CurrentState = PlayerStates.AtRest;

    public GameObject FishModel;
    private static float IntensityDecreaseRate = 5f;
    private static float IntensityIncreaseRate = 15f;
    private static float MaxIntensity = 50;
    private static float MaxLungCapacity = 150f;
    private static float RateOfBreathReplenishment = 25f;

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

    private void MoveFishModel()
    {
        if (FishModel != null && CurrentBreathPressure > 0) {
            FishModel.transform.Translate(0, 0, CurrentBreathPressure*Time.deltaTime);
        }
    }

    // Use this for initialization
    private void Start() {}

    // Update is called once per frame
    private void Update()
    {
        var buttonVal = Input.GetButton(GetButtonCode(Control));
        if (buttonVal) {
            if (CurrentState == PlayerStates.AtRest) {
                CurrentState = PlayerStates.Blowing;
            }
        }
        else {
            CurrentState = PlayerStates.AtRest;
        }
        UpdateBreathPressure();
        MoveFishModel();
        UpdateHud();
    }

    private void UpdateBreathPressure()
    {
        if (CurrentState == PlayerStates.Blowing) {
            CurrentBreathPressure += IntensityIncreaseRate*Time.deltaTime;
        }
        else {
            CurrentBreathPressure -= IntensityDecreaseRate*Time.deltaTime;
        }
        CurrentBreathPressure = Mathf.Clamp(CurrentBreathPressure, 0, MaxIntensity);
    }

    private void UpdateHud()
    {
        if (BreathDisplay != null) {
            BreathDisplay.text = String.Format("{0}/{1} | {2}", CurrentLungCapacity, MaxLungCapacity,
                CurrentBreathPressure);
        }
    }
}