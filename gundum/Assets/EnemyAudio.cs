using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> hitSounds;
    private List<AudioSource> sourceList;//single shot sound sources.

    void Awake()
    {
        InitAudio();
    }

    void InitAudio()
    {
        sourceList = new List<AudioSource>();

        foreach (AudioClip c in hitSounds)
        {
            AudioSource s = gameObject.AddComponent<AudioSource>();
            s.clip = c;
            sourceList.Add(s);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("Player")) //Deal damage to player with enemy bullets
        {
            //play random grunt sound
            int index = Random.Range(0, hitSounds.Count - 1);
            sourceList[index].Play();
        }
    }
}
