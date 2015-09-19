using UnityEngine;

public class FishMachine : MonoBehaviour {
    public PlayerMachine.PlayerIdentifier Player;
    // Use this for initialization
    private void Start() {}

    // Update is called once per frame
    private void Update() {}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("fish detected OnCollisionEnter");
    }

    private void OnCollisionStay(Collision col)
    {
        Debug.Log("fish detected OnCollisionStay");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("fish detected OnTriggerEnter");
    }
}