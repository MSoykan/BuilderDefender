using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance { get; private set; }


    public enum Sound {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver
    }
    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;
    private float volume = .5f;

    private void Awake() {
        instance = this;

        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat("soundVolume", .5f);

        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        foreach (Sound sound in System.Enum.GetValues(typeof(Sound))) {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound, float volumeScale) {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound], volume);
    }

    public float GetVolume() {
        return volume;
    }

    public void IncreaseVolume() {
        volume += .1f;
        volume = Mathf.Clamp01(volume);
    }

    public void DecreaseVolume() {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);

    }
}
