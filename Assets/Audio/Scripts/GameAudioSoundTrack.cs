using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Audio/SoundTrack")]
public class GameAudioSoundTrack : ScriptableObject
{
    public AudioClip titleScreenBMG;
    public AudioClip dungeonexploreBMG;
    public AudioClip townBMG;

    public List<AudioClip> BattleSoundTrack;
    public List<AudioClip> bossBGM;

    public AudioClip afterCombatBGM;
    public AudioClip gameOverSoundEffect;
}
