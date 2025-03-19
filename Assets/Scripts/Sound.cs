using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;               // Sound name
    public AudioClip clip;            // Sound file

    [Range(0f, 1f)]
    public float volume = 0.7f;       // Volum

    [Range(0.5f, 1.5f)]
    public float pitch = 1f;          // Pitch

    public bool loop = false;         // Loop

    [HideInInspector]
    public AudioSource source;        // AudioSource
}
