using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string id;
        public AudioClip clip;
    }

    [SerializeField]
    private List<Sound> sounds;
    [SerializeField]
    private AudioSource audioSource;

    public void PlaySound(string soundID)
    {
        if (audioSource.isPlaying)
            return;
        // looks for the sound with the same ID
        foreach (Sound sound in sounds)
        {
            if (sound.id == soundID)
            {
                audioSource.PlayOneShot(sound.clip);
                return;
            }
        }
        Debug.LogWarning("sound notfound " + soundID);
    }
}