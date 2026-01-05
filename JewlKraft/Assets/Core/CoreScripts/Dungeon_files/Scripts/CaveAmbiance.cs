using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CaveAmbience : MonoBehaviour
{
    [Header("Ambient Sounds")]
    public AudioClip[] caveSounds;

    [Header("Random Settings")]
    [SerializeField] private float minDelay = 3f;
    [SerializeField] private float maxDelay = 10f;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.6f;

    private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup caveMixerGroup;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = caveMixerGroup;
        StartCoroutine(PlayAmbientSounds());
    }

    private IEnumerator PlayAmbientSounds()
    {
        while (true)
        {
            // Pick random cave sound
            AudioClip clip = caveSounds[Random.Range(0, caveSounds.Length)];

            // Play it
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(clip, volume);

            // Wait random time before next sound
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
        }
    }
}
