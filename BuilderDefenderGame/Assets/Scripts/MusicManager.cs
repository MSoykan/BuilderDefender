using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{

    private AudioSource audioSource;

    private float volume= .5f;


    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }
    public float GetVolume() {
        return volume;
    }

    public void IncreaseVolume() {
        volume += .1f;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
    }

    public void DecreaseVolume() {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
    }
}
