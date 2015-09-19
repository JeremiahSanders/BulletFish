using UnityEngine;

public class FinishLine : MonoBehaviour {
    public GameObject GameController;

    private GameMachine GameMachine
    {
        get { return GameController == null ? null : GameController.GetComponent<GameMachine>(); }
    }

    private static bool IsFish(GameObject gameObject)
    {
        return gameObject != null && gameObject.GetComponent<FishMachine>() != null;
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("finish line detected OnCollisionEnter");
    }

    private void OnCollisionStay(Collision col)
    {
        Debug.Log("finish line detected OnCollisionStay");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("finish line detected OnTriggerEnter");
        if (IsFish(other.gameObject)) {
            Debug.Log("finish line detected fish");
            GameMachine.ReachedFinishLine(other.gameObject.GetComponent<FishMachine>().Player);
            Destroy(other.gameObject);
        }
    }

    // Use this for initialization
    private void Start() {}

    // Update is called once per frame
    private void Update() {}
}