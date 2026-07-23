using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager instance;

    [HideInInspector] public UnityEvent playUIMovement;
    [HideInInspector] public UnityEvent playUISelect;
    [HideInInspector] public UnityEvent playUIBack;

    [Space(10)]
    [Header("SoundTracks")]
    [SerializeField] private GameAudioSoundTrack soundTrack;
    private float BGMTimeStamp;
    private AudioClip SoundBeforeCombatBGM; //save audio before battle

    [Space(10)]
    [Header("SoundEffects")]
    [SerializeField] private List<SoundEffectsGroup> soundEffects;
    public Dictionary<string, AudioClip> SoundEffectLookUpTable;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            BuildSoundEffects();

            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void BuildSoundEffects()
    {
        SoundEffectLookUpTable = new Dictionary<string, AudioClip>();
        for(int i = 0; i < soundEffects.Count; i++)
        {
            for(int y = 0; y < soundEffects[i].soundEffects.Count; y++)
            {
                var soundClip = soundEffects[i].soundEffects[y];
                SoundEffectLookUpTable[soundClip.key] = soundClip.clip;
            }
        }
    }

    private void Start()
    {
        playUIMovement.AddListener(() => PlaySoundEffect(SoundEffectLookUpTable["menu_move"]));
        playUISelect.AddListener(() => PlaySoundEffect(SoundEffectLookUpTable["menu_click"]));
        playUIBack.AddListener(() => PlaySoundEffect(SoundEffectLookUpTable["menu_back"]));
    }

    public void PlayTitleScreenBMG()
    {
        SoundBeforeCombatBGM = AudioController.Instance.GetAudioClip();
        BGMTimeStamp = AudioController.Instance.CurrentTimeStampOnAudioClip();
        AudioController.Instance.PlayTrack(soundTrack.titleScreenBMG, true);
    }
    public void PlayDropShipMenuBMG()
    {
        SoundBeforeCombatBGM = AudioController.Instance.GetAudioClip();
        BGMTimeStamp = AudioController.Instance.CurrentTimeStampOnAudioClip();
        AudioController.Instance.PlayTrack(soundTrack.dropshipBMG, true);
    }
    public void PlayDungeonTrack()
    {
        if (AudioController.Instance.GetAudioClip() != soundTrack.dungeonexploreBMG)
        {
            AudioController.Instance.PlayTrack(soundTrack.dungeonexploreBMG, true);
        }
    }
    public void PlayRandomBattleMusic()
    {
        SoundBeforeCombatBGM = AudioController.Instance.GetAudioClip();
        BGMTimeStamp = AudioController.Instance.CurrentTimeStampOnAudioClip();
        var choice = Random.Range(0, soundTrack.BattleSoundTrack.Count);
        AudioController.Instance.PlayTrack(soundTrack.BattleSoundTrack[choice], true);
    }
    public void PlayBattleMusic(int index)
    {
        SoundBeforeCombatBGM = AudioController.Instance.GetAudioClip();
        BGMTimeStamp = AudioController.Instance.CurrentTimeStampOnAudioClip();
        AudioController.Instance.PlayTrack(soundTrack.BattleSoundTrack[index], true);
    }
    public void PlayBossMusic(int index)
    {
        SoundBeforeCombatBGM = AudioController.Instance.GetAudioClip();
        BGMTimeStamp = AudioController.Instance.CurrentTimeStampOnAudioClip();
        AudioController.Instance.PlayTrack(soundTrack.bossBGM[index], true);
    }

    /*
    public void PlayTownTrack(int index)
    {
        if(AudioController.Instance.GetAudioClip() != soundTrack.TownBGM[index])
        {
            AudioController.Instance.PlayTrack(soundTrack.TownBGM[index], true);
        }
    }
    
    */
    public void PauseBGM()
    {
        SoundBeforeCombatBGM = AudioController.Instance.GetAudioClip();
        BGMTimeStamp = AudioController.Instance.CurrentTimeStampOnAudioClip();
        AudioController.Instance.PauseBGM();
    }
    public void StopBGM()
    {
        AudioController.Instance.StopBGM();
    }
    public void PlayGameOverSoundEffect()
    {
        AudioController.Instance.PlaySoundEffect(soundTrack.gameOverSoundEffect);
    }
    public void PlayAfterBattleBGM()
    {
        AudioController.Instance.PlayTrack(soundTrack.afterCombatBGM, true);
    }

    public void PlayerOutSideCombatBGM()
    {
        AudioController.Instance.PlayTrack(SoundBeforeCombatBGM, true, BGMTimeStamp);
    }
    public void PlaySoundEffect(string key)
    {
        if (SoundEffectLookUpTable.ContainsKey(key))
        {
            PlaySoundEffect(SoundEffectLookUpTable[key]);
        }
    }
    public void PlaySoundEffectChannelTwo(string key) //damage soundEffect channel
    {
        if (SoundEffectLookUpTable.ContainsKey(key))
        {
            PlaySoundEffectChannelTwo(SoundEffectLookUpTable[key]);
        }
    }
    private void PlaySoundEffect(AudioClip audioClip)
    {
        AudioController.Instance.PlaySoundEffect(audioClip);
    }
    private void PlaySoundEffectChannelTwo(AudioClip audioClip)
    {
        AudioController.Instance.PlaySoundEffectChannelTwo(audioClip);
    }
}
