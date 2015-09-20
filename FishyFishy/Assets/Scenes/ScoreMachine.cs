using System;
using UnityEngine;
using System.Collections;

public class ScoreMachine : MonoBehaviour {
    public GameObject FirstPlaceLocation;
    public GameObject SecondPlaceLocation;
    public GameObject ThirdPlaceLocation;
    public GameObject FourthPlaceLocation;
    public GameObject PlayerOneFishSprite;
    public GameObject PlayerThreeFishSprite;
    public GameObject PlayerTwoFishSprite;
    public GameObject PlayerFourFishSprite;
    public GameObject AudioPlayer;
    private AudioMachine AudioMachine { get
    {
        return AudioPlayer == null ? null : AudioPlayer.GetComponent<AudioMachine>();
    } }

    // Use this for initialization
    void Start () {
        if (GameMachine.Winners == null) return;
        var totalWinners = GameMachine.Winners.Count;
        for (int i = 0; i < totalWinners; i++) {
            if (i >3)break;
            GameObject fishObject = null;
            switch (GameMachine.Winners[i]) {
                case PlayerMachine.PlayerIdentifier.Player1:
                    fishObject = PlayerOneFishSprite;
                    break;
                case PlayerMachine.PlayerIdentifier.Player2:
                    fishObject = PlayerTwoFishSprite;
                    break;
                case PlayerMachine.PlayerIdentifier.Player3:
                    fishObject = PlayerThreeFishSprite;
                    break;
                case PlayerMachine.PlayerIdentifier.Player4:
                    fishObject = PlayerFourFishSprite;
                    break;
                default:break;
            }
            if (fishObject == null) return;
            switch (i) {
                case 0:
                    fishObject.transform.position = FirstPlaceLocation.transform.position;
                    break;
                case 1:
                    fishObject.transform.position = SecondPlaceLocation.transform.position;
                    break;
                case 2:
                    fishObject.transform.position = ThirdPlaceLocation.transform.position;
                    break;
                case 3:
                    fishObject.transform.position = FourthPlaceLocation.transform.position;
                    break;
                default:
                    break;
            }
        }
        if (AudioMachine==null)return;
        AudioMachine.PlayRandomSomeoneWonQuip();
    }

    // Update is called once per frame
    private void Update() {}
}
