using UnityEngine;
using System.Collections;
using Complete;

public class AudioManager : MonoBehaviour
{
    public float masterVolumePercent;
    public float sfxVolumePercent;
  
    public static AudioManager instance;

    Transform audioListener;
    Transform playerT;

    SoundLibrary library;

    void Awake()
    {
        instance = this;

        audioListener = FindObjectOfType<AudioListener>().transform;
            if (FindObjectOfType<PlayerManager>() != null)
            {
                playerT = FindObjectOfType<PlayerManager>().transform;
            }
        
    }

    void Update()
    {
        if (playerT != null)
        {
            audioListener.position = playerT.position;
        }
    }


    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, audioListener.position, 1.0f);
        }
    }

}