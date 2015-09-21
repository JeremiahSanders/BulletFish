using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioMachine : MonoBehaviour {
    private readonly List<AudioClip> QueuedClips = new List<AudioClip>();
    public AudioClip[] GameStartClips;
    public AudioClip[] RandomInGameQuips;
    public AudioClip[] SomeoneWonQuips;
    public AudioClip[] MainMenuClips;

    private AudioClip GetRandomClip(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, (audioClips.Length))];
    }

    public void PlayRandomInGameQuip()
    {
        if (RandomInGameQuips == null || !RandomInGameQuips.Any()) return;
        QueuedClips.Add(GetRandomClip(RandomInGameQuips));
    }

    public void PlayRandomIntroClip()
    {
        if (GameStartClips == null || !GameStartClips.Any()) return;
        QueuedClips.Add(GetRandomClip(GameStartClips));
    }

    public void PlayRandomSomeoneWonQuip()
    {
        if (SomeoneWonQuips == null || !SomeoneWonQuips.Any()) return;
        QueuedClips.Add(GetRandomClip(SomeoneWonQuips));
    }

    public void PlayMainMenuClips()
    {
        if(MainMenuClips == null)return;
        foreach (var clip in MainMenuClips) {
            QueuedClips.Add(clip);
        }
    }

    private void PlaySound()
    {
        if (!QueuedClips.Any()) return;
        var clip = QueuedClips[0];
        QueuedClips.RemoveAt(0);
        var src = GetComponent<AudioSource>();
        src.PlayOneShot(clip);
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