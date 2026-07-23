using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Audio/SoundEffects")]
public class SoundEffectsGroup : ScriptableObject
{
    public List<SoundEffectsNode> soundEffects;
}
