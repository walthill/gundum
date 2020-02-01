using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechAudioScr : MonoBehaviour
{
    //[SerializeField]
    public List<AudioClip> DamageSounds;
    [SerializeField]
    List<AudioSource> DMGS;
    // Start is called before the first frame update
    void Start()
    {
        //DMGS = new List<AudioSource>();
        foreach(AudioClip c in DamageSounds)
        {
            Debug.Log(c.name);
            AudioSource s;
            s = gameObject.AddComponent<AudioSource>();
            
            DMGS.Add(s);
            s.clip = c;
        }
        
    }

    public void DamageSound(int DMGSoundINDEX)
    {
        DMGS[DMGSoundINDEX].Play();
    }
}
