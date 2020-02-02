using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudioScr : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> SingleShotSounds;// jumpSound, shootSound, hurtSound;
    [SerializeField]
    AudioClip walkingSound;
    List<AudioSource> SSSS;//single shot sound sources.
    AudioSource walkSource;

    

    // Start is called before the first frame update
    void Start()
    {
        SSSS = new List<AudioSource>();
        walkSource = gameObject.AddComponent<AudioSource>();
        walkSource.clip = walkingSound;
        walkSource.loop = true;
        walkSource.volume = .5f;

        foreach(AudioClip c in SingleShotSounds)
        {
            AudioSource s = gameObject.AddComponent<AudioSource>();
            s.clip = c;
            SSSS.Add(s);
        }
    }
    public void PlaySoundByIndex(int SoundIndex)
    {
        SSSS[SoundIndex].Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startWalking()
    {
        if (!walkSource.isPlaying)
        {
            walkSource.Play();
        }
    }
    public void StopWalking()
    {
        if (walkSource.isPlaying)
        {
            walkSource.Stop();
        }
    }
}
