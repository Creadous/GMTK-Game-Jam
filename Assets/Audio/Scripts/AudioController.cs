using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance; 
    public AudioSource BGM_audioSource;
    public AudioSource soundEffect_audioSource;
    public AudioSource soundEffect_audioSourceTwo; //done this way to the multiply sound effect can be played like hit and damage
    [Range(1,100)]
    public float masterVolmue = 100.0f;
    [Range(1, 100)]
    public float SFXVolume = 100.0f;
    [Range(1, 100)]
    public float BGMVolume = 100.0f;
    [Range(1, 100)]
    public float characterVolume = 100.0f;

    //public List<MovementSoundCollection> movementSoundEffect;
    public int footStepIndex;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
    public void Start()
    {
        //first setUp
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            Instance.masterVolmue = PlayerPrefs.GetFloat("masterVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("masterVolume", AudioController.Instance.masterVolmue);
        }
        //first setUp
        if (PlayerPrefs.HasKey("backgroundMusicVolume"))
        {
            Instance.BGMVolume = PlayerPrefs.GetFloat("backgroundMusicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("SFXVolume", AudioController.Instance.BGMVolume);
        }
        //first setUp
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            Instance.SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("backgroundMusicVolume", AudioController.Instance.SFXVolume);
        }
        //first setUp
        if (PlayerPrefs.HasKey("CharacterVoiceVolume"))
        {
            Instance.characterVolume = PlayerPrefs.GetFloat("CharacterVoiceVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("CharacterVoiceVolume", AudioController.Instance.characterVolume);
        }
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        soundEffect_audioSource.volume = GetSoundEffectVolume();
        soundEffect_audioSource.PlayOneShot(clip);
    }
    public void PlaySoundEffectChannelTwo(AudioClip clip)
    {
        soundEffect_audioSourceTwo.volume = GetSoundEffectVolume();
        soundEffect_audioSourceTwo.PlayOneShot(clip);
    }
    public void PlayTrack(AudioClip clip, bool loop)
    {
        BGM_audioSource.volume = GetBGMVolume();
        BGM_audioSource.time = 0;
        BGM_audioSource.clip = clip;
        BGM_audioSource.loop = loop;
        BGM_audioSource.Play();
    }
    public void PlayTrack(AudioClip clip, bool loop, float timeStamp)
    {
        BGM_audioSource.volume = GetBGMVolume();
        BGM_audioSource.clip = clip;
        BGM_audioSource.loop = loop;
        BGM_audioSource.time = timeStamp;
        BGM_audioSource.Play();
    }
    public float GetBGMVolume()
    {
        return (BGMVolume / 100.0f) * (masterVolmue/ 100.0f);
    }
    public float GetSoundEffectVolume()
    {
        return (Instance.SFXVolume / 100.0f) * (masterVolmue / 100.0f);
    }
    public AudioClip GetAudioClip()
    {
        return BGM_audioSource.clip;
    }
    public float CurrentTimeStampOnAudioClip()
    {
        return BGM_audioSource.time;
    }
    public void StopBGM()
    {
        BGM_audioSource.Stop();
        BGM_audioSource.clip = null; // dont store it. full stop
    }
    public void StopSoundEffect()
    {
        soundEffect_audioSource.Stop();
        soundEffect_audioSource.clip = null;
        soundEffect_audioSourceTwo.Stop();
        soundEffect_audioSourceTwo.clip = null;
    }
    public void PauseBGM()
    {
        BGM_audioSource.Pause();
    }
    public void ResumeBGM()
    {
        BGM_audioSource.Play();
    }
    public void UpdateBGMVolume()
    {
        BGM_audioSource.volume = GetBGMVolume();
    }
    /*
    public void PlayFootStep(bool player, TerrainType terrain)
    {
      
        for(int i = 0; i < movementSoundEffect.Count; i++)
        {
            if (movementSoundEffect[i].HasPassedConditions(player,terrain))
            {
                soundEffect_audioSourceTwo.PlayOneShot(movementSoundEffect[i].foodStepSounds[footStepIndex]);
                footStepIndex = UtilitesHelper.CycleNumber(footStepIndex, movementSoundEffect[i].foodStepSounds.Count, 1);
                break;
            }
        }
        
    }
    
    public void PlayBounceWallSoundEffect(TerrainType terrain)
    {
        for (int i = 0; i < movementSoundEffect.Count; i++)
        {
            if (movementSoundEffect[i].HasPassedConditions(true, terrain))
            {
                soundEffect_audioSource.PlayOneShot(movementSoundEffect[i].wallBounce);
                break;
            }
        }

    }
    */
}
