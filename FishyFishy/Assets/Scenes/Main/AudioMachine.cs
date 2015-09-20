using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioMachine : MonoBehaviour {
    public AudioClip[] GameStartClips;
    public AudioClip[] RandomInGameQuips;
    private readonly List<AudioClip> QueuedClips = new List<AudioClip>();

    private void PlaySound()
    {
        if (!QueuedClips.Any()) return;
        var clip = QueuedClips[0];
        QueuedClips.RemoveAt(0);
        var src = GetComponent<AudioSource>();
        src.PlayOneShot(clip);
    }

    public void PlayRandomIntroClip()
    {
        if (GameStartClips == null || !GameStartClips.Any()) return;
        QueuedClips.Add(GetRandomClip(GameStartClips));
    }

    private AudioClip GetRandomClip(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, (audioClips.Length))];
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

    public void PlayRandomInGameQuip()
    {
        if (RandomInGameQuips == null || !RandomInGameQuips.Any()) return;
        QueuedClips.Add(GetRandomClip(RandomInGameQuips));
    }
}