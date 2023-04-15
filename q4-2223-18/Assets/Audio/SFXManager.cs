using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource sfxSource;
    [Header("Audio Clips")]
    public AudioClip[] audioClips;
    public AudioClip[] attackSounds; 

    /// <summary>
    /// <para><b>IDS:</b></para>
    /// <para>1 = buttonHover</para>
    /// <para>2 = enterPressed</para>
    ///  <para>3 = menuExit</para>
    /// </summary>
    /// <param name="id"></param>
    public void playAudio(int id)
    {
        sfxSource.clip = audioClips[id - 1]; 
        sfxSource.Play();
    }

}
