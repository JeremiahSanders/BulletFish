using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {
    public Slider VolumeSlider;

    public void OnVolumeSliderChanged()
    {
        if (VolumeSlider == null) return;
        AudioListener.volume = VolumeSlider.value;
    }

    // Use this for initialization
    private void Start() {}

    // Update is called once per frame
    private void Update() {}
}