﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Facilitates all sound effects and music. Only one is allowed per scene.
public class AudioHelper : MonoBehaviour {

    public static Dictionary<string, AudioClip> staticClips;
    public List<AudioClip> audioClips;
    private static AudioSource StaticGetAudioSource () {
        return GameObject.FindObjectOfType<AudioHelper> ().GetComponent<AudioSource> ();
    }
    public static void PlaySound (string soundName, bool isLoop) {
        AudioSource src = StaticGetAudioSource ();
        src.loop = isLoop;
        src.clip = staticClips[soundName];
        src.Play ();
    }
    public static void Stop () {
        StaticGetAudioSource ().Stop ();
        StaticGetAudioSource ().loop = false;
    }

    // Start is called before the first frame update
    void Start () {
        staticClips = new Dictionary<string, AudioClip> ();
        foreach (AudioClip clip in audioClips) {
            staticClips.Add (clip.name, clip);
        }
    }

    // Update is called once per frame
    void Update () {

    }
}