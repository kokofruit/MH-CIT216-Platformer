using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public GameObject soundPlayerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, float pitch = 1, float volume = 1)
    {
        // Instantiate an object to play the sound as a child
        GameObject soundPlayer = Instantiate(soundPlayerPrefab, transform.position, Quaternion.identity, transform);

        // Get the audiosource component. if there is none, early return
        AudioSource soundPlayerAudio = soundPlayer.GetComponent<AudioSource>();
        if (soundPlayerAudio == null) return;

        // Set the clip, pitch, and volume of the audiosource
        soundPlayerAudio.clip = clip;
        soundPlayerAudio.pitch = pitch;
        soundPlayerAudio.volume = volume;

        // Play the audio
        soundPlayerAudio.Play();

        // Destroy the object after its done playing
        float playTime = clip.length;
        Destroy(soundPlayer, playTime);
    }
}
