using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Preferences")]
    public float volume; 
    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource attackSFXSource; 
    [Header("Audio Clips")]
    public AudioClip[] audioClips;
    public AudioClip[] attackSounds;

    /// <summary>
    /// <para><b>IDS:</b></para>
    /// <para>1 = buttonHover</para>
    /// <para>2 = enterPressed</para>
    ///  <para>3 = menuExit</para>
    ///  <para>4 = talking_Dumb</para>
    ///  <para>5 = talking_Evil</para>
    ///  <para>6 = talking_Narrator</para>
    ///  <para>7 = talking_Friend</para>
    /// </summary>
    /// <param name="id"></param>
    public void playAudio(int id)
    {
        sfxSource.Stop(); 
        sfxSource.clip = audioClips[id - 1];
       // sfxSource.volume = volume; 
        sfxSource.Play();
       //StartCoroutine(volumeFade(sfxSource, 0,volume, sfxSource.clip.length)); 

    }
    /// <summary>
    /// <para><b>IDS:</b></para>
    /// <para>1 = Slash</para>
    /// <para>2 = Bash</para>
    /// <para>3 = Magic Dart</para>
    /// <para>4 = Heal</para>
    /// </summary>
    /// <param name="id"></param>
    public void playAttackAudio(int id)
    {
        attackSFXSource.Stop();
        Debug.Log("playing attack audio"); 
        attackSFXSource.clip = attackSounds[id - 1];
        //sfxSource.volume = volume;
        attackSFXSource.Play();
    }
   /*IEnumerator volumeFade(AudioSource aSource, float endVolume,float startVolume, float fadeLength) 
    {

        aSource.volume = startVolume; 
        float startTime = Time.time;
        while (Time.time < startTime + fadeLength)
        {
            aSource.volume = startVolume + ((endVolume - startVolume) * ((Time.time - startTime) / fadeLength));
            yield return null;
        }
        if (endVolume == 0)
        {
            aSource.Stop();
        }

    }*/

}
