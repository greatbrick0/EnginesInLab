using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField]
    private List<AudioClip> audioClips = new List<AudioClip>();
    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            foreach (AudioClip ii in audioClips)
            {
                audioSources.Add(gameObject.AddComponent<AudioSource>());
                audioSources[^1].clip = ii;
            }
        }
    }

    public void PlaySoundByIndex(int index)
    {
        audioSources[index].Play();
    }
}
