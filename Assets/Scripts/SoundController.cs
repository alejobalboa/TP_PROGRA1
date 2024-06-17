using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    //Audio
    [SerializeField] private AudioSource audioSourceOnce;
    [SerializeField] private AudioSource audioSourceLoop;
 
    // Start is called before the first frame update
    void Start()
    {
        //Audio
        //Sound effect volume preference
        if (audioSourceOnce != null)
        {
            audioSourceOnce.volume = PlayerPrefs.GetFloat("VolumenGeneral", 1.0f);
        }
        if (audioSourceLoop != null)
        {
            audioSourceLoop.volume = PlayerPrefs.GetFloat("VolumenGeneral", 1.0f);
        }
    }

    private void Update()
    {
        audioSourceOnce.volume = PlayerPrefs.GetFloat("VolumenGeneral", 0.3f);
        audioSourceLoop.volume = PlayerPrefs.GetFloat("VolumenGeneral", 0.3f);
    }


    //Play single sound once
    public void PlaySound(AudioClip clip)
    {
        audioSourceOnce.loop = false;
        audioSourceOnce.PlayOneShot(clip);
    }
    //Play sound on loop
    public void PlaySoundLoop(AudioClip clip)
    {
        audioSourceLoop.loop = true;
        audioSourceLoop.clip = clip;
        audioSourceLoop.Play();
    }
    //Stop looping sound
    public void StopLoopSound()
    {
        audioSourceLoop.Stop();
    }
}
