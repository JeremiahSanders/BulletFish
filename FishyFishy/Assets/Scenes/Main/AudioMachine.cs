using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioMachine : MonoBehaviour {
    private readonly List<AudioClip> QueuedClips = new List<AudioClip>();

    private void PlaySound()
    {
        if (!QueuedClips.Any()) return;
        var clip = QueuedClips[0];
        QueuedClips.RemoveAt(0);
        var src = GetComponent<AudioSource>();
        src.PlayOneShot(clip);
    }

    public void PlayVoiceoverClip(AudioClip clip)
    {
        if (clip == null) return;
        QueuedClips.Add(clip);
    }

    // Use this for initialization
    private void Start() {}

    // Update is called once per frame
    private void Update()
    {
        var src = GetComponent<AudioSource>();
        if (src == null || src.isPlaying) return;
        PlaySound();
    }
}