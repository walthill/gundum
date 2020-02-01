using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechAudioScr : MonoBehaviour
{
    //[SerializeField]
    public List<AudioClip> DamageSounds;
    //[SerializeField]
    List<AudioSource> DMGS;
    [SerializeField]
    List<AudioClip> WarningSounds;//impact incomming,system fail, breach
    List<AudioSource> WSS;
    // Start is called before the first frame update
    void Start()
    {
        DMGS = new List<AudioSource>();
        WSS = new List<AudioSource>();
        foreach (AudioClip c in DamageSounds)
        {
            Debug.Log(c.name);
            AudioSource s;
            s = gameObject.AddComponent<AudioSource>();
            
            DMGS.Add(s);
            s.clip = c;
        }
        foreach(AudioClip c in WarningSounds)
        {
            AudioSource s = gameObject.AddComponent<AudioSource>();
            s.clip = c;
            WSS.Add(s);
        }
        
    }

    public void DamageSound(int DMGSoundINDEX)
    {
        DMGS[DMGSoundINDEX].Play();
    }
    public void PlayWarningSound(int soundIndex)
    {
        WSS[soundIndex].Play();
    }
}
