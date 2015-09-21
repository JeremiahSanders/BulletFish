using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {
    public Slider VolumeSlider;
    private bool screenLoaded;
    public void OnVolumeSliderChanged()
    {
        if (!screenLoaded) return;
        if (VolumeSlider == null) return;
        AudioListener.volume = VolumeSlider.value;
    }

    // Use this for initialization
    private void Start()
    {
        if (VolumeSlider == null) return;
        VolumeSlider.value = AudioListener.volume;
        screenLoaded = true;
    }

    // Update is called once per frame
    private void Update() {}
}