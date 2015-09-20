using UnityEngine;

public class FishMachine : MonoBehaviour {
    private const float BASE_SPEED = 0.045f;
    private const float MAX_SPEED = 8f;
    private const float SLOWDOWN_RATE = 0.27f;
    private float _currentSpeed;
    public GameObject PlayerController;

    private bool shouldMove = true;
    public GameObject SwimLane;

    public PlayerMachine.PlayerIdentifier Player
    {
        get { return PlayerMachine.Control; }
    }

    private PlayerMachine PlayerMachine
    {
        get { return PlayerController != null ? PlayerController.GetComponent<PlayerMachine>() : null; }
    }

    private float CreateSpeed()
    {
        var baseSpeed = (BASE_SPEED*Time.deltaTime);
        var pm = PlayerMachine;
        var speed = pm.IsBlowing
            ? baseSpeed + GetBlowingSpeedModifier(pm)
            : baseSpeed + (_currentSpeed*Time.deltaTime*SLOWDOWN_RATE);
        return Mathf.Clamp(speed, 0, MAX_SPEED);
    }

    private float GetBlowingSpeedModifier(PlayerMachine pm)
    {
        const float blowingEffectPercentage = 0.64f;
        const float blowingFactor = 3.4f;
        const float fearThreshold = 0.85f;
        return pm.CurrentBreathPressure > fearThreshold
            ? (BASE_SPEED*blowingEffectPercentage)*-1
            : BASE_SPEED*blowingEffectPercentage*blowingFactor*pm.CurrentBreathPressure;
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("fish detected OnCollisionEnter");
    }

    private void OnCollisionStay(Collision col)
    {
        Debug.Log("fish detected OnCollisionStay");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("fish detected OnTriggerEnter");
        if (other.gameObject != null && other.gameObject.GetComponent<FinishLine>() != null) {
            // reached the finish line
            shouldMove = false;
        }
    }

    // Use this for initialization
    private void Start() {}
    // Update is called once per frame
    private void Update()
    {
        if (!shouldMove) return;
        var newSpeed = CreateSpeed();
        SwimLane.transform.Translate(0, newSpeed, 0);
        _currentSpeed = newSpeed;
    }
}