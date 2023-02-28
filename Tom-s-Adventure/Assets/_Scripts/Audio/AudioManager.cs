using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    //Sound Effects
    public AudioClip deathSound;
    public AudioClip jumpSound;
    public AudioClip collectSound;
    public AudioClip finishSound;
    //Music
    public AudioClip musicSound;

    //Music Object
    private float playbackPosition;
    public GameObject currentMusicObject;
    //Sound Object
    public GameObject soundObject;
    protected override void Awake()
    {
        this.MakeSingleton(false);
    }
    public void PlaySFX(EffectSound sfxName)
    {
        switch (sfxName)
        {
            case EffectSound.DeathSound:
                SoundObjectCreation(deathSound);
                break;
            case EffectSound.JumpSound:
                SoundObjectCreation(jumpSound);
                break;
            case EffectSound.CollectItemSound:
                SoundObjectCreation(collectSound);
                break;
            case EffectSound.FinishSound:
                SoundObjectCreation(finishSound);
                break;
            default:
                break;
        }
    }

    void SoundObjectCreation(AudioClip clip)
    {
        //Create SoundsObject gameobject
        GameObject newObject = Instantiate(soundObject, transform);
        //Destroy gameobject when sound is not playing
        newObject.AddComponent<KillSound>();
        //Assign audioclip to its audiosource
        newObject.GetComponent<AudioSource>().clip = clip;
        //Play the audio
        newObject.GetComponent<AudioSource>().Play();
    }
    public void PlayMusic(MusicSound musicName)
    {
        switch (musicName)
        {
            case MusicSound.BackgroundSound:
                MusicObjectCreation(musicSound);
                break;
            default:
                break;
        }
    }

    public void StopCurrentMusic(bool stop)
    {
        AudioSource audioSource = currentMusicObject.GetComponent<AudioSource>();
        if (stop && audioSource.isPlaying)
        {
            playbackPosition = audioSource.time;
            audioSource.Stop();
        }
        else if (!stop && !audioSource.isPlaying)
        {
            // Later, resume the audio from where it left off
            audioSource.time = playbackPosition;
            audioSource.Play();
        }
    }

    void MusicObjectCreation(AudioClip clip)
    {
        //Check if there's an existing music object, if so delete it
        if (currentMusicObject)
            Destroy(currentMusicObject);
        //Create SoundsObject gameobject
        currentMusicObject = Instantiate(soundObject, transform);
        //Assign audioclip to its audiosource
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        //Make the audio source looping
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        //Play the audio
        currentMusicObject.GetComponent<AudioSource>().Play();
    }
}