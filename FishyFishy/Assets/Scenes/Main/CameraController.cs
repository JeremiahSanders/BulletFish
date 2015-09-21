using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject Fish1;
    public GameObject Fish2;
    public GameObject Fish3;
    public GameObject Fish4;

    private float GetMinZ()
    {
        var minZ = 0f;
        if (Fish1 != null && Fish1.transform.forward.z < minZ) minZ = Fish1.transform.forward.z;
        if (Fish2 != null && Fish2.transform.forward.z < minZ) minZ = Fish2.transform.forward.z;
        if (Fish3 != null && Fish3.transform.forward.z < minZ) minZ = Fish3.transform.forward.z;
        if (Fish4 != null && Fish4.transform.forward.z < minZ) minZ = Fish4.transform.forward.z;
        return minZ;
    }

    // Use this for initialization
    private void Start() {}

    // Update is called once per frame
    private void Update() {}
}